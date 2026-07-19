using vHolidays.Models.Base;

namespace vHolidays.Models.Package.DatabaseModel
{
    public class PackageInclusions : BaseEntity
    {
        public string Name { get; set; }
        public bool IsIncluded { get; set; }

        public int PackageDetailId { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;
    }
}
