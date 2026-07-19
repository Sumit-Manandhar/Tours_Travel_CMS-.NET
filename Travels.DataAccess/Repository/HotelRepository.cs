using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Repository.IRepository;
using Travels.DataAcess.Data;
using Travels.Models.Common;
using Travels.Models.Hotel;

namespace Travels.DataAccess.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly ApplicationDbContext _db;
        public HotelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public List<Hotel> GetHotels(string? keyword = "")
        {
            return _db.Hotel.Where(a=>a.HotelName.ToLower().Contains(keyword??"".ToLower())).Include(a => a.Country).Include(a => a.City).ToList();
        }
        public List<Hotel> GetActiveten(int countryId, int? CityId = 0)
        {
            return _db.Hotel.Where(a => (a.IsActive && a.IsPublished && !a.IsDeleted)
            && (countryId == 0 || a.CountryId == countryId)
            && (CityId == null || CityId.HasValue || CityId.Value == 0 || CityId.Value == a.CityId))
                .Include(a => a.Country).Include(a => a.City).ToList();
        }
        public async Task<ResponseModel<int>> CreateUpdate(HotelListViewModel model)
        {
            try
            {
                Hotel data = new()
                {
                    Id = model.Id,
                    HotelName = model.HotelName,
                    AddressLine = model.AddressLine,
                    CountryId = model.CountryId,
                    DisplayImageUrl = model.DisplayImageUrl,
                    CityId = model.CityId,
                    IsActive = model.IsActive,
                    IsPublished = model.IsPublished,
                    IsDeleted = model.IsDeleted,
                };
                Update(data);
                await _db.SaveChangesAsync();

                return new ResponseModel<int>
                {
                    Data = data.Id,
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
        public async Task<ResponseModel<int>> OnOffDelete(int Id)
        {
            var obj = _db.Hotel.FirstOrDefault(a => a.Id == Id);
            if (obj is null)
                return new ResponseModel<int>
                {
                    Succeeded = false,
                    Message = "Invalid Hotel",
                };
            obj.IsDeleted = !obj.IsDeleted;
            _db.Update(obj);
            await _db.SaveChangesAsync();
            return new ResponseModel<int>
            {
                Data = Id,
                Succeeded = true,
                Message = "Hotel Saved",
            };
        }
    }
}
