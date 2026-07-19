namespace vHolidays.Models.Package.DataTransferObjects
{
    public class PackageListModel
    {
        public bool IsGroupBooking { get; set; }
        public List<PackageDetailViewModel> DatList { get; set; }
    }
}
