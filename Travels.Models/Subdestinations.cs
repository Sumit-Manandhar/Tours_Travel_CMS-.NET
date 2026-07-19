using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Travels.Models.Base;
using Travels.Models.Regions;

namespace Travels.Models
{
    public class Subdestinations : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        [DisplayName("Destination Name")]
        public string Name { get; set; }
        public int CityId { get; set; }
        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; } = null!;

        public virtual City City { get; set; } = null!;
        public SubdestinationData SubestinationData { get; set; } = new SubdestinationData();

        public virtual ICollection<SubdestinationImage> SubdestinationImage { get; set; } = new List<SubdestinationImage>();

    }
}
