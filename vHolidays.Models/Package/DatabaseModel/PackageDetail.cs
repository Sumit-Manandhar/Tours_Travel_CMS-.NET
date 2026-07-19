using vHolidays.Models.Base;
using vHolidays.Models.Regions;

namespace vHolidays.Models.Package.DatabaseModel
{
    public class PackageDetail : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Day { get; set; }
        public int Night { get; set; }
        public int MinAge { get; set; }
        public DateOnly Validity { get; set; }
        public decimal Rating { get; set; }
        public DateOnly? StartDate { get; set; }
        public bool IsGroupBooking { get; set; }

        public string PackageSummary { get; set; }
        public string TermsAndCondition { get; set; }
        public string PackageImgUrl { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<PackageItinerary> Itinerary { get; set; } = new List<PackageItinerary>();
        public virtual ICollection<PackageInclusions> Inclusion { get; set; } = new List<PackageInclusions>();
        public virtual ICollection<PackageType> PackageType { get; set; } = new List<PackageType>();
        public virtual ICollection<PackageClassOption> PackageClassOption { get; set; } = new List<PackageClassOption>();
        public virtual ICollection<PackageImage> PackageImage { get; set; } = new List<PackageImage>();
        public virtual ICollection<PackageGroup> PackageGroup { get; set; } = new List<PackageGroup>();
        public virtual ICollection<PackagePrice> PackagePrice { get; set; } = new List<PackagePrice>();
        public virtual ICollection<PackageHotel> PackageHotel { get; set; } = new List<PackageHotel>();
        public virtual ICollection<PackageFlight> PackageFlight { get; set; } = new List<PackageFlight>();
    }
}
