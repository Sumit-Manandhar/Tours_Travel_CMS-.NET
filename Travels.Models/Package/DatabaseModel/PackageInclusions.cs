using Travels.Models.Base;

namespace Travels.Models.Package.DatabaseModel
{
    public class PackageInclusions : BaseEntity
    {
        public string Name { get; set; }
        public bool IsIncluded { get; set; }

        public int PackageDetailId { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;
    }
}
