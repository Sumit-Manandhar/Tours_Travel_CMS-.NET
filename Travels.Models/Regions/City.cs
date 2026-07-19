using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Travels.Models.Base;
using Travels.Models.Hotel;

namespace Travels.Models.Regions
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
        public virtual ICollection<Travels.Models.Hotel.Hotel> Hotel { get; set; } = new List<Travels.Models.Hotel.Hotel>();
        public virtual ICollection<Travels.Models.TailorTrip.DatabaseModel.TailorMadeTrip> TailorMadeTrip { get; set; } = new List<Travels.Models.TailorTrip.DatabaseModel.TailorMadeTrip>();

    }
}
