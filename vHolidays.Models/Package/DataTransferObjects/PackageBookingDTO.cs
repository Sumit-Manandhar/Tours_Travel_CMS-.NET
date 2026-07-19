using System.ComponentModel.DataAnnotations;
using vHolidays.Models.Regions;

namespace vHolidays.Models.Package.DataTransferObjects
{
    public class PackageBookingDTO
    {
        public int PackageId { get; set; }
        // Package Details
        [Required(ErrorMessage = "Arrival Date is required")]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }

        [Required(ErrorMessage = "Departure Date is required")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "At least one adult is required")]
        public int NumberOfAdults { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of children cannot be negative")]
        public int NumberOfChildren { get; set; }

        // Food Preferences
        public bool Vegetarian { get; set; }
        public bool NonVegetarian { get; set; }

        [Required(ErrorMessage = "Special Request is required")]
        public string SpecialRequest { get; set; }

        // Personal Details
        [Required(ErrorMessage = "Salutation is required")]
        public string Salutation { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "Invalid contact number")]
        public string ContactNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Document type is required")]
        public string DocumentType { get; set; }

        // Emergency Details
        [Required(ErrorMessage = "Salutation is required")]
        public string EmergencySalutation { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string EmergencyFirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string EmergencyLastName { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        public string EmergencyNationality { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string EmergencyAddress { get; set; }

        [Phone(ErrorMessage = "Invalid contact number")]
        public string EmergencyContactNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmergencyEmailAddress { get; set; }

        [Required(ErrorMessage = "Relation is required")]
        public string EmergencyRelation { get; set; }

        // Flight Preference
        [Required(ErrorMessage = "Flight preference is required")]
        public bool NeedFlight { get; set; }
        public List<Country> Countries { get; set; }
        public bool IsGroupBooking { get; set; }
    }
}


