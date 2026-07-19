using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using vHolidays.Models.Base;
using vHolidays.Models.Hotel;

namespace vHolidays.Models.Regions
{
    [Table("City", Schema = "Region")]
    public class City :BaseEntity
    {
   
        [MaxLength(100)]
        public string CityName { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public bool IsPrimary { get; set; } = true;
        public virtual ICollection<vHolidays.Models.Hotel.Hotel> Hotel { get; set; } = new List<vHolidays.Models.Hotel.Hotel>();
        public virtual ICollection<vHolidays.Models.TailorTrip.DatabaseModel.TailorMadeTrip> TailorMadeTrip { get; set; } = new List<vHolidays.Models.TailorTrip.DatabaseModel.TailorMadeTrip>();

    }
}
