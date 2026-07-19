using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Travels.Models.Base;

namespace Travels.Models.Regions
{
    [Table("State", Schema = "Region")]
    public class State :BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string? StateCode { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
