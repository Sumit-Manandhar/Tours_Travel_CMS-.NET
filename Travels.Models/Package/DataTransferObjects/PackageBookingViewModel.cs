using System.ComponentModel.DataAnnotations;

namespace Travels.Models.Package.DataTransferObjects
{
    public class PackageBookingViewModel
    {
        // Package Details
        public int PackageId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Day { get; set; }
        public int Night { get; set; }
        public DateOnly? StartDate { get; set; }
        public bool IsGroupBooking { get; set; }

        // Package Booking Details
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
        public string Destination { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }

        // Food Preferences
        public bool Vegetarian { get; set; }
        public bool NonVegetarian { get; set; }
        public string SpecialRequest { get; set; }

        // Personal Details
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string DocumentType { get; set; }
        public string EmergencySalutation { get; set; }
        public string EmergencyFirstName { get; set; }
        public string EmergencyLastName { get; set; }
        public string EmergencyNationality { get; set; }
        public string EmergencyAddress { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyEmailAddress { get; set; }
        public string EmergencyRelation { get; set; }
        public bool NeedFlight { get; set; }

        //Status
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
