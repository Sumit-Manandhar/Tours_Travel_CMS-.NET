namespace Travels.Models.Package.DataTransferObjects
{
    public class PackagePriceDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int PackageDetailId { get; set; }
        public int PackageClassOptionId { get; set; }
        public int PackageGroupId { get; set; }
    }
}
