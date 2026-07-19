using System.ComponentModel.DataAnnotations.Schema;
using vHolidays.Models.Base;
using vHolidays.Models.Hotel;

namespace vHolidays.Models.Package.DatabaseModel
{
    public class PackageHotel : BaseEntity
    {
      
        public int PackageId { get; set; }
        public virtual PackageDetail Package { get; set; } = null!;

        public int HotelId { get; set; }
        public virtual vHolidays.Models.Hotel.Hotel Hotel { get; set; } = null!;


    }
}
