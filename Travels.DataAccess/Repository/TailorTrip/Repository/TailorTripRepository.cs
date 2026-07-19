using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Repository.TailorTrip.Interface;
using Travels.DataAcess.Data;
using Travels.Models.Common;
using Travels.Models.TailorTrip.DatabaseModel;
using Travels.Models.TailorTrip.DataTransferObjects;

namespace Travels.DataAccess.Repository.TailorTrip.Repository
{
    public class TailorTripRepository : Repository<TailorMadeTrip>, ITailorTripRepository
    {
        private readonly ApplicationDbContext _db;

        public TailorTripRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<string>> Create(TailorTripCreateModel model)
        {
            try
            {
                TailorMadeTrip data = new()
                {
                    TravelDate = model.TravelDate,
                    Length = model.Length,
                    HotelCategory = model.HotelCategory,
                    Activity = model.Activity,
                    Adult = model.Adult,
                    Child = model.Child,
                    Infant = model.Infant,
                    CountryId = model.CountryId,
                    CityId = model.CityId,
                    IsActive = true,
                    IsPublished = true,
                    PersonalDetail = new TripPersonalDetail()
                    {
                        Salutation = model.PersonalDetail.Salutation,
                        FirstName = model.PersonalDetail.FirstName,
                        LastName = model.PersonalDetail.LastName,
                        Email = model.PersonalDetail.Email,
                        PhoneNumber = model.PersonalDetail.PhoneNumber,
                        SpecialRequest = model.PersonalDetail.SpecialRequest,
                        CountryId = model.PersonalDetail.CountryId,
                        IsActive = true,
                        IsPublished = true,
                    }
                };
                Add(data);
                await _db.SaveChangesAsync();
                return new ResponseModel<string>()
                {
                    Succeeded = true,
                    Message = "Trip request has been created. We will get back to you."
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>()
                {
                    Succeeded = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<TailorTripDto> Detail(int id)
        {
            var data = await _db.TailorMadeTrip.Where(x => x.Id == id).Include(x => x.PersonalDetail).ThenInclude(x => x.Country)
                .Include(x => x.Country)
                .Include(x=>x.City).Select(x => new TailorTripDto()
                {
                    TravelDate = x.TravelDate,
                    Length = x.Length,
                    HotelCategory = x.HotelCategory,
                    Activity = x.Activity,
                    Adult = x.Adult,
                    Child = x.Child,
                    Infant = x.Infant,
                    CountryId = x.CountryId,
                    Country = x.Country.CountryName,
                    CityId = x.CityId,
                    City=x.City.CityName,
                    PersonalDetail = new TailorTripPersonalDetailDto()
                    {
                        Salutation = x.PersonalDetail.Salutation,
                        FirstName = x.PersonalDetail.FirstName,
                        LastName = x.PersonalDetail.LastName,
                        Email = x.PersonalDetail.Email,
                        PhoneNumber = x.PersonalDetail.PhoneNumber,
                        SpecialRequest = x.PersonalDetail.SpecialRequest,
                        CountryId = x.PersonalDetail.CountryId,
                        Country = x.PersonalDetail.Country.CountryName,
                    }
                }).FirstOrDefaultAsync();
            return data;
        }
    }
}
