using System.ComponentModel.DataAnnotations.Schema;
using Travels.Models.Base;
using Travels.Models.Hotel;

namespace Travels.Models.Package.DatabaseModel
{
    public class PackageHotel : BaseEntity
    {
      
        public int PackageId { get; set; }
        public virtual PackageDetail Package { get; set; } = null!;

        public int HotelId { get; set; }
        public virtual Travels.Models.Hotel.Hotel Hotel { get; set; } = null!;


    }
}
