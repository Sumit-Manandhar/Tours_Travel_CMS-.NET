using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Travels.Models.Base;
using System.ComponentModel;
using Travels.DataAccess.Context;

namespace Travels.Models.Regions
{
    [Table("Country", Schema = "Region")]
    public class Country :BaseEntity
    {
        [Required]
        [DisplayName("Country Name")]
        public required string CountryName { get; set; }
        [MaxLength(2)]
        public string? Alpha2Code { get; set; }
        [MaxLength(3)]
        public string? Alpha3Code { get; set; }
        public string DialCode { get; set; }
        public string Flag { get; set; }
        public string Currency { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<State>? State { get; set; }
        public virtual ICollection<DestinationData> DestinationData { get; set; } = new List<DestinationData>();
        public virtual ICollection<Travels.Models.Hotel.Hotel> Hotel { get; set; } = new List<Travels.Models.Hotel.Hotel>();

    }
}
