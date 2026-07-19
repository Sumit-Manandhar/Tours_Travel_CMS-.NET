using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Settings;
using vHolidays.Models.Settings.DataTransferObjects;

namespace vHolidays.DataAccess.Repository
{
    public class BranchAgentsRepository : Repository<BranchAgents>, IBranchAgentsRepository
    {
        private readonly ApplicationDbContext _db;

        public BranchAgentsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(AgentsDTO Model)
        {
            if(Model.Id> 0)
            {
                var entity = _db.BranchAgents.FirstOrDefault(a => a.Id == Model.Id);
                if (entity is null)
                    throw new InvalidDataException();
                entity.IsActive = Model.IsActive;
                entity.IsPublished = Model.IsPublished;
                entity.AgentName = Model.AgentName;
                entity.Address = Model.Address;
                entity.Email = Model.Email;
                entity.LiscenceNo = Model.LiscenceNo;
                _db.Update(entity);
                await _db.SaveChangesAsync();
            }
            else
            {
                var entity = new BranchAgents()
                {
                    IsActive = Model.IsActive,
                    IsDeleted = false,
                    AgentName = Model.AgentName,
                    Address = Model.Address,
                    Email = Model.Email,
                    LiscenceNo = Model.LiscenceNo,
                    ContactBranchId = Model.ContactBranchId,
                    IsPublished = Model.IsPublished,
                    CreatedDate = DateTime.Now,
                };
                await _db.BranchAgents.AddAsync(entity);
                await _db.SaveChangesAsync();
                Model.Id = entity.Id;
            }

           return Model.Id;
        }

        public async Task<AgentsDTO> Get(int id)
        {
            var data = await _db.BranchAgents.FirstOrDefaultAsync(a => a.Id == id);
            if(data is null)
                return new AgentsDTO();
            return new AgentsDTO
            {
                Id = data.Id,
                IsActive = data.IsActive,
                IsPublished = data.IsActive,
                AgentName = data.AgentName,
                Address = data.Address,
                Email = data.Email,
                LiscenceNo= data.LiscenceNo,
                ContactBranchId= data.ContactBranchId,
            };
        }

        public async Task<List<BranchAgents>> GetAll(int id)
        {
            return await _db.BranchAgents.Where(a => a.ContactBranchId == id).ToListAsync();
        }
        public async Task OnOffDelete(int id)
        {
            var obj = _db.BranchAgents.FirstOrDefault(a => a.Id == id);
            if (obj is null)
                throw new InvalidDataException("invalid Id");
            obj.IsDeleted = !obj.IsDeleted;
            _db.Update(obj);
            await _db.SaveChangesAsync();
        }
    }
}
