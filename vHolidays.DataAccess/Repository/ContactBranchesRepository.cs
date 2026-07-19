using Microsoft.EntityFrameworkCore;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Settings;
using vHolidays.Models.Settings.DataTransferObjects;

namespace vHolidays.DataAccess.Repository;

public class ContactBranchesRepository : Repository<ContactBranches>, IContactBranchesRepository
{
    private readonly ApplicationDbContext _db;
    public ContactBranchesRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public async Task<List<ContactBranches>> GetAll()
    {
        var branchesWithAgents = await _db.ContactBranches
            .Include(branch => branch.BranchAgents) // Load related BranchAgents
            .ToListAsync();

        return branchesWithAgents;
    }

    public async Task Update(ContactBranches contactBranches)
    {
        _db.Update(contactBranches);
       await  _db.SaveChangesAsync();
    }
    public async Task<int> AddUpdate(BranchesDTO model)
    {
        if (model.Id > 0)
        {
            var entity = _db.ContactBranches.FirstOrDefault(a => a.Id == model.Id);
            if (entity is null)
            {
                throw new InvalidDataException();
            }
            entity.Id = model.Id;
            entity.BranchName = model.BranchName;
            entity.BranchCountry = model.BranchCountry;
            entity.BranchType = model.BranchType;
            entity.IsActive = model.IsActive;
            entity.IsPublished = model.IsPublished;
            entity.Address = model.Address;
            entity.ContactNumber = model.ContactNumber;
            entity.Email = model.Email;
            entity.OptionalNumber = model.OptionalNumber;
            if (!string.IsNullOrEmpty(model.ImageURL))
                entity.CompanyImage = model.ImageURL;
            entity.ModifiedBy = model.UserName;
            entity.ModifiedDate = DateTime.UtcNow;

            await Update(entity);
        }
        else
        {
            var entity = new ContactBranches()
            {
                BranchName = model.BranchName,
                BranchCountry = model.BranchCountry,
                BranchType = model.BranchType,
                IsActive = model.IsActive,
                IsPublished = model.IsPublished,
                Address = model.Address,
                ContactNumber = model.ContactNumber,
                Email = model.Email,
                IsDeleted = false,
                OptionalNumber = model.OptionalNumber,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = model.UserName,
                CompanyImage = model.ImageURL,
            };
            _db.ContactBranches.Add(entity);
            await _db.SaveChangesAsync();
            model.Id = entity.Id;
        }
        return model.Id;
    }
    public async Task OnOffDelete(int DestinationId)
    {
        var obj = _db.ContactBranches.FirstOrDefault(a => a.Id == DestinationId);
        if (obj is null)
            throw new KeyNotFoundException("invalid Id");
        obj.IsDeleted = !obj.IsDeleted;
        _db.Update(obj);
        await _db.SaveChangesAsync();
    }
}

