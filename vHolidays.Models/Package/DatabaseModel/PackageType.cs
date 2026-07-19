using vHolidays.Models.Base;
using vHolidays.Models.Master.DatabaseModel;

namespace vHolidays.Models.Package.DatabaseModel
{
    public class PackageType : BaseEntity
    {
        public int PackageDetailId { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;

        public int TourTypeId { get; set; }
        public virtual TourType TourType { get; set; } = null!;
    }
}
