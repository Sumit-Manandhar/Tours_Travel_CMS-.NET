using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Repository.Package.Interface;
using Travels.DataAcess.Data;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Repository
{
    public class PackageHotelRepository : Repository<PackageHotel>, IPackageHotelRepository
    {
        private readonly ApplicationDbContext _db;
        public PackageHotelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<int>> CreateUpdate(PackageHotelViewModel model)
        {
            try
            {
                foreach (var hotel in model.HotelId)
                {
                    PackageHotel data = new()
                    {
                        Id = 0,
                        PackageId = model.PackageId,
                        HotelId = hotel,
                        IsActive = true,
                        IsPublished = true,
                        IsDeleted = true,
                    };
                    Update(data);
                }
                if (model.ToRemoveHotelIds?.Count() > 0)
                    _db.RemoveRange(_db.PackageHotel.Where(a => model.ToRemoveHotelIds.Contains(a.HotelId)));
                await _db.SaveChangesAsync();

                return new ResponseModel<int>
                {
                    Data = 0,
                    Succeeded = true,
                    Message = "Hotel Saved",
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

        public async Task<ResponseModel<object>> DeleteHotelPackages(int HotelId, int packageId)
        {
            try
            {
                var data = _db.PackageHotel.Where(a => a.HotelId == HotelId && a.PackageId== packageId);
                if(data == null)
                    return new ResponseModel<object>
                    {
                        Succeeded = false,
                        Message = "Invalid data",
                    };
                _db.PackageHotel.RemoveRange(data);
              await  _db.SaveChangesAsync();
                return new ResponseModel<object>
                {
                    Succeeded = true,
                    Message = "Hotel removed from Your package",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<object>
                {
                    Succeeded = false,
                    Message = "Cannot remove the hotel. Please try again",
                };
            }
        }
        public async Task<List<HotelPackageViewModel>> PackageHotel(int PackageDetailId)
        {
            var data = await (from pkg in GetAll(x => x.PackageId == PackageDetailId)
                              join hot in _db.Hotel.Include(x => x.Country).Include(x => x.City) on pkg.HotelId equals hot.Id
                              select new HotelPackageViewModel()
                              {
                                  City = hot.City.CityName,
                                  AddressLine = hot.AddressLine,
                                  Country = hot.Country.CountryName,
                                  HotelName = hot.HotelName,
                                  Url = hot.DisplayImageUrl
                              }).ToListAsync();
            return data;
        }
    }
}
