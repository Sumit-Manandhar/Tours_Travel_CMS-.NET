namespace vHolidays.Models.Package.DataTransferObjects
{
    public class InclusionAddUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIncluded { get; set; }
        public int PackageDetailId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
    }
    public class SaveIncusionModel
    {
        public int PackageDetailId { get; set; }
        public List<InclusionAddUpdateModel> Inclusions { get; set; }
    }
}
