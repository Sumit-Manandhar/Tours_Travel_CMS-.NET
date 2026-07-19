using Travels.Models.Base;
using Travels.Models.Master.DatabaseModel;

namespace Travels.Models.Package.DatabaseModel
{
    public class PackageClassOption : BaseEntity
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public int PackageDetailId { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;

        public int ClassOptionId { get; set; }
        public virtual ClassOption ClassOption { get; set; } = null!;
        public virtual ICollection<PackagePrice> PackagePrice { get; set; } = new List<PackagePrice>();
    }
}
