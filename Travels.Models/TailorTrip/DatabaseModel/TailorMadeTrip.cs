using Travels.Models.Base;
using Travels.Models.Regions;

namespace Travels.Models.TailorTrip.DatabaseModel
{
    public class TailorMadeTrip : BaseEntity
    {
        public DateOnly TravelDate { get; set; }
        public int Length { get; set; }
        public string HotelCategory { get; set; }
        public string Activity { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public int CountryId { get; set; } // Destination
       
        public virtual Country Country { get; set; } = null!;
        public int? CityId { get; set; }
        public virtual City City { get; set; } = null!;

        public virtual TripPersonalDetail PersonalDetail { get; set; } = new TripPersonalDetail();
    }
}
