using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Repository.IRepository;
using Travels.DataAcess.Data;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository
{
    public class BookingRepository : Repository<PackageBooking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<int>> BookNow(PackageBookingDTO model)
        {
            try
            {
                var data = new PackageBooking()
                {
                    PackageId = model.PackageId,
                    ArrivalDate = model.ArrivalDate,
                    DepartureDate = model.DepartureDate,
                    Destination = model.Destination,
                    NumberOfAdults = model.NumberOfAdults,
                    NumberOfChildren = model.NumberOfChildren,
                    Vegetarian = model.Vegetarian,
                    NonVegetarian = model.NonVegetarian,
                    SpecialRequest = model.SpecialRequest,
                    Salutation = model.Salutation,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Nationality = model.Nationality,
                    Address = model.Address,
                    ContactNumber = model.ContactNumber,
                    EmailAddress = model.EmailAddress,
                    DocumentType = model.DocumentType,
                    EmergencySalutation = model.EmergencySalutation,
                    EmergencyFirstName = model.EmergencyFirstName,
                    EmergencyLastName = model.EmergencyLastName,
                    EmergencyNationality = model.EmergencyNationality,
                    EmergencyAddress = model.EmergencyAddress,
                    EmergencyContactNumber = model.EmergencyContactNumber,
                    EmergencyEmailAddress = model.EmergencyEmailAddress,
                    EmergencyRelation = model.EmergencyRelation,
                    NeedFlight = model.NeedFlight,
                };
                _db.PackageBookings.Add(data);
                await _db.SaveChangesAsync();

                return new ResponseModel<int>
                {
                    Data = data.Id,
                    Succeeded = true,
                    Message = "Package Booked",
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

        public Task<PackageBookingViewModel> GetDetail(int Id)
        {
            var data = (from booking in _db.PackageBookings.Where(x => x.Id == Id)
                        join pkg in _db.PackageDetails on booking.PackageId equals pkg.Id
                        select new PackageBookingViewModel()
                        {
                            PackageId = booking.PackageId,
                            Name = pkg.Name,
                            Code = pkg.Code,
                            Day = pkg.Day,
                            Night = pkg.Night,
                            StartDate = pkg.StartDate,
                            IsGroupBooking = pkg.IsGroupBooking,
                            ArrivalDate = booking.ArrivalDate,
                            DepartureDate = booking.DepartureDate,
                            Destination = booking.Destination,
                            NumberOfAdults = booking.NumberOfAdults,
                            NumberOfChildren = booking.NumberOfChildren,
                            Vegetarian = booking.Vegetarian,
                            NonVegetarian = booking.NonVegetarian,
                            SpecialRequest = booking.SpecialRequest,
                            Salutation = booking.Salutation,
                            FirstName = booking.FirstName,
                            LastName = booking.LastName,
                            Nationality = booking.Nationality,
                            Address = booking.Address,
                            ContactNumber = booking.ContactNumber,
                            EmailAddress = booking.EmailAddress,
                            DocumentType = booking.DocumentType,
                            EmergencySalutation = booking.EmergencySalutation,
                            EmergencyFirstName = booking.EmergencyFirstName,
                            EmergencyLastName = booking.EmergencyLastName,
                            EmergencyNationality = booking.EmergencyNationality,
                            EmergencyAddress = booking.EmergencyAddress,
                            EmergencyContactNumber = booking.EmergencyContactNumber,
                            EmergencyEmailAddress = booking.EmergencyEmailAddress,
                            EmergencyRelation = booking.EmergencyRelation,
                            NeedFlight = booking.NeedFlight,
                            CreatedBy = booking.CreatedBy,
                            CreatedDate = booking.CreatedDate,
                            ModifiedBy = booking.ModifiedBy,
                            ModifiedDate = booking.ModifiedDate,
                            Id = booking.Id,
                            IsActive = booking.IsActive,
                            IsDeleted = booking.IsDeleted,
                            IsPublished = booking.IsPublished
                        }).FirstOrDefaultAsync();
            return data;
        }
    }
}
