using Travels.Models.Base;
using Travels.Models.Master.DatabaseModel;

namespace Travels.Models.Package.DatabaseModel
{
    public class PackageType : BaseEntity
    {
        public int PackageDetailId { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;

        public int TourTypeId { get; set; }
        public virtual TourType TourType { get; set; } = null!;
    }
}
