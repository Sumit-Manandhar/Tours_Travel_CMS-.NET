using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Travels.Models.Master.DatabaseModel;
using Travels.Models.Regions;
using Travels.Utility.CustomAttrubute;
using Travels.Utility.Enumerations;

namespace Travels.Models.Package.DataTransferObjects
{
    public class PackageDetailAddUpdateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public int Day { get; set; }
        public int Night { get; set; }
        public int MinAge { get; set; }
        public DateOnly Validity { get; set; }
        public decimal Rating { get; set; }
        [Required(ErrorMessage = "Package Summary is required.")]
        public string PackageSummary { get; set; }
        [Required(ErrorMessage = "Terms & Condition is required.")]
        public string TermsAndCondition { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public int CountryId { get; set; }
        public List<Country> Countries { get; set; }
        public IEnumerable<int> TourTypeId { get; set; }
        public IEnumerable<int> ClassOptionId { get; set; }
        public string? BaseUrl { get; set; } = string.Empty;
        public IFormFile? PackageImg { get; set; }
        public string PackageImgUrl { get; set; } = string.Empty;

        [RequiredIfNot("PackageType", (int)GroupPackageTypeEnum.Package, ErrorMessage = "Start Date required for Group Booking.")]
        public DateOnly? StartDate { get; set; }
        public GroupPackageTypeEnum PackageType { get; set; }

        public List<TourType> TourTypes { get; set; }
        public List<ClassOption> ClassOptionTypes { get; set; }

    }
}
