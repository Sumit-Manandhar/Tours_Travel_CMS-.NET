using vHolidays.Models.Base;
using vHolidays.Models.Regions;

namespace vHolidays.Models.TailorTrip.DatabaseModel
{
    public class TripPersonalDetail : BaseEntity
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SpecialRequest { get; set; }

        public int CountryId { get; set; } //nationality
        public virtual Country Country { get; set; } = null!;

        public int TailorMadeTripId { get; set; }
        public virtual TailorMadeTrip TailorMadeTrip { get; set; }
    }
}
