namespace vHolidays.Models.Package.DataTransferObjects
{
    public class GroupAddUpdateModel
    {
        public int Id { get; set; }
        public int Adult { get; set; }
        public int FreeOfCost { get; set; }
        public bool SingleSupliment { get; set; }
        public int PackageDetailId { get; set; }
        public int Order { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
    }
}
