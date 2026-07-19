namespace Travels.Models.Package.DataTransferObjects
{
    public class PriceViewModel
    {
        public List<PackagePriceDto> PriceList { get; set; }
        public List<PackageClassOptionDto>? ClassOption { get; set; }
        public List<PackageGroupDto>? GroupList { get; set; }
    }
}
