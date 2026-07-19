using Microsoft.EntityFrameworkCore;
using vHolidays.DataAccess.Repository.Package.Interface;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Repository
{
    internal class PackageInclusionsReposotory : Repository<PackageInclusions>, IPackageInclusionsReposotory
    {
        private readonly ApplicationDbContext _db;
        public PackageInclusionsReposotory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<int>> CreateUpdate(List<InclusionAddUpdateModel> model)
        {
            try
            {
                foreach (var i in model)
                {
                    PackageInclusions data = new()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        IsIncluded = i.IsIncluded,
                        PackageDetailId = i.PackageDetailId,
                        IsActive = i.IsActive,
                        IsPublished = i.IsPublished,
                        IsDeleted = i.IsDeleted,
                    };
                    Update(data);
                    await _db.SaveChangesAsync();
                    i.Id = data.Id;

                }
                var allEntity = _db.PackageInclusions.Where(a => a.PackageDetailId == model.FirstOrDefault().PackageDetailId).ToList();

                var toDelete = allEntity.Where(a => !model.Select(c => c.Id).Contains(a.Id)).ToList();
                if (toDelete.Count > 0)
                {
                    toDelete.ForEach(x => x.IsDeleted = true);
                    _db.PackageInclusions.UpdateRange(toDelete);
                }
                await _db.SaveChangesAsync();
                return new ResponseModel<int>
                {
                    Data = 0,
                    Succeeded = true,
                    Message = "Inclusion Saved",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<int>
                {
                    Succeeded = false,
                    Message = ex.Message,
                };
            }

        }

        public async Task<List<PackageInclusions>> Detail(int PackageDetailId)
        {
            var data = await _db.PackageInclusions.Where(x => x.PackageDetailId == PackageDetailId && !x.IsDeleted).Select(x => new PackageInclusions()
            {
                IsIncluded = x.IsIncluded,
                Name = x.Name
            }).ToListAsync();
            return data;
        }

        public async Task<InclusionAddUpdateModel> Get(int Id)
        {
            var model = Get(x => x.Id == Id);
            return new InclusionAddUpdateModel()
            {
                Id = Id,
                Name = model.Name,
                IsIncluded = model.IsIncluded,
                PackageDetailId = model.PackageDetailId,
                IsActive = model.IsActive,
                IsPublished = model.IsPublished,
                IsDeleted = model.IsDeleted,
            };
        }

    }
}
