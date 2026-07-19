using vHolidays.Models.Base;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Regions;

namespace vHolidays.Models.Hotel
{
    public class Hotel : BaseEntity
    {
        public string HotelName { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;


        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;

        public string DisplayImageUrl { get; set; }
        public string AddressLine { get; set; }

        public virtual ICollection<PackageHotel> PackageHotel { get; set; } = new List<PackageHotel>();


    }
}
