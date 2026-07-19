using Travels.Models.Base;

namespace Travels.Models.Package.DatabaseModel
{
    public class PackagePrice : BaseEntity
    {
        public double Price { get; set; }

        public int PackageDetailId { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;

        public int PackageClassOptionId { get; set; }
        public virtual PackageClassOption PackageClassOption { get; set; } = null!;

        public int PackageGroupId { get; set; }
        public virtual PackageGroup PackageGroup { get; set; } = null!;
    }
}
