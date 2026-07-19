using System.ComponentModel.DataAnnotations;
using Travels.Models.Base;

namespace Travels.Models.Package.DatabaseModel
{
    public class PackageBooking : BaseEntity
    {
        public int PackageId { get; set; }

        // Package Details
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
    }
}
