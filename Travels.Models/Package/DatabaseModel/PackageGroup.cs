using Travels.Models.Base;

namespace Travels.Models.Package.DatabaseModel
{
    public class PackageGroup : BaseEntity
    {
        public int Adult { get; set; }
        public int FreeOfCost { get; set; }
        public bool SingleSupliment { get; set; }
        public int PackageDetailId { get; set; }
        public int Order { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;
        public virtual ICollection<PackagePrice> PackagePrice { get; set; } = new List<PackagePrice>();
    }
}
