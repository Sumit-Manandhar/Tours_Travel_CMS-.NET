
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using vHolidays.DataAccess.Context;
using vHolidays.Models.Base;

namespace vHolidays.Models
{
    public class Destination :BaseEntity
    {
        [Required]
        [MaxLength(30)]
        [DisplayName("Destination Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
        public virtual ICollection<Subdestinations> Subdestination { get; set; } = new List<Subdestinations>();

        public DestinationData DestinationData { get; set; } = new DestinationData();

        public virtual ICollection<DestinationImage> DestinationImages { get; set; } = new List<DestinationImage>();
    }
}
