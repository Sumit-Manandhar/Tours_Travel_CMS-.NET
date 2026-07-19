using Travels.Models.Base;

namespace Travels.Models.Package.DatabaseModel
{
    public class PackageItinerary : BaseEntity
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PackageDetailId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
    }
}
