using Travels.DataAccess.Repository.IRepository;
using Travels.DataAcess.Data;
using Travels.Models.Common;
using Travels.Models.Master.DatabaseModel;
using Travels.Models.Master.DataTransferObjects;

namespace Travels.DataAccess.Repository
{
    public class TourTypeRepository : Repository<TourType>, ITourTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public TourTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ResponseModel<int>> CreateUpdate(TourTypeDTO model)
        {
            try
            {
                TourType data = new()
                {
                    Id = model.Id,
                    Description = model.Description,
                    Name = model.Name,
                    IsActive = model.IsActive,
                    IsPublished = model.IsPublished,
                    IsDeleted = false,
                };
                if (model.Id > 0)
                {
                    Update(data);
                }
                else
                {

                    await _db.AddAsync(data);
                }

                await _db.SaveChangesAsync();
                return new ResponseModel<int>
                {
                    Data = data.Id,
                    Succeeded = true,
                    Message = "Tour Type Saved",
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
        public async Task<ResponseModel<object>> OnOffDelete(int id)
        {
            try
            {
                var obj = _db.TourType.FirstOrDefault(a => a.Id == id);
                if (obj is null)
                    return new ResponseModel<object>
                    {
                        Succeeded = false,
                        Message = "Entity not found",
                    };
                obj.IsDeleted = !obj.IsDeleted;
                _db.Update(obj);
                await _db.SaveChangesAsync();
                return new ResponseModel<object>
                {
                    Succeeded = true,
                    Message = "Tour Type Saved",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<object>
                {
                    Succeeded = false,
                    Message = ex.Message,
                };
            }

        }
    }
}
