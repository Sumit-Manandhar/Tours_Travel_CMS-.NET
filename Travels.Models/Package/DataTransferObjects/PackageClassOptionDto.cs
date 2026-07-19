namespace Travels.Models.Package.DataTransferObjects
{
    public class PackageClassOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public int PackageDetailId { get; set; }
    }
}
