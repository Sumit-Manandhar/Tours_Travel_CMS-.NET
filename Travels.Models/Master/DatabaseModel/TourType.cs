using Travels.Models.Base;

namespace Travels.Models.Master.DatabaseModel
{
    public class TourType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
