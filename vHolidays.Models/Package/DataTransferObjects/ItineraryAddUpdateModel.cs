namespace vHolidays.Models.Package.DataTransferObjects
{
    public class ItineraryAddUpdateModel
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
    public class SaveItineraryModel
    {
        public int PackageDetailId { get; set; }
       public List<ItineraryAddUpdateModel> Itineraries { get; set; }
    }
}
