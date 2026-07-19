using Microsoft.EntityFrameworkCore;
using vHolidays.DataAccess.Repository.Package.Interface;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Repository
{
    public class PackageItineraryReposotory : Repository<PackageItinerary>, IPackageItineraryReposotory
    {
        private readonly ApplicationDbContext _db;
        public PackageItineraryReposotory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<int>> CreateUpdate(List<ItineraryAddUpdateModel> items)
        {
            try
            {
                foreach (var model in items)
                {
                    PackageItinerary data = new()
                    {
                        Id = model.Id,
                        Day = model.Day,
                        Name = model.Name,
                        Description = model.Description,
                        PackageDetailId = model.PackageDetailId,
                        IsActive = model.IsActive,
                        IsPublished = model.IsPublished,
                        IsDeleted = model.IsDeleted,
                    };
                    Update(data);
                    await _db.SaveChangesAsync();
                    model.Id = data.Id;
                }

                var allEntity = _db.PackageItineraryss.Where(a => a.PackageDetailId == items.FirstOrDefault().PackageDetailId).ToList();

                var toDelete = allEntity.Where(a => !items.Select(c => c.Id).Contains(a.Id)).ToList();
                if (toDelete.Count > 0)
                {
                    toDelete.ForEach(x => x.IsDeleted = true);
                    _db.PackageItineraryss.UpdateRange(toDelete);
                }
                await _db.SaveChangesAsync();

                return new ResponseModel<int>
                {
                    Data = 0,
                    Succeeded = true,
                    Message = "Itinerary Saved",
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

        public async Task<List<PackageItineraryViewModel>> Detail(int PackageDetailId)
        {
            var data = _db.PackageItineraryss.Where(x => x.PackageDetailId == PackageDetailId && !x.IsDeleted).Select(x => new PackageItineraryViewModel()
            {
                Day = x.Day,
                Name = x.Name,
                Description = x.Description
            }).ToList();
            return data;
        }

        public async Task<ItineraryAddUpdateModel> Get(int Id)
        {
            var model = Get(x => x.Id == Id);
            return new ItineraryAddUpdateModel()
            {
                Id = Id,
                Name = model.Name,
                Day = model.Day,
                Description = model.Description,
                PackageDetailId = model.PackageDetailId,
                IsActive = model.IsActive,
                IsPublished = model.IsPublished,
                IsDeleted = model.IsDeleted,
            };
        }
    }
}
