using Microsoft.AspNetCore.Http;
using Travels.Models.Regions;
using Travels.Models.ViewModels;

namespace Travels.Models.Destinations.DataTransferObjects
{
    public class DestinationAddUpdateModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? CountryName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public List<ImageViewModel> Images { get; set; } = new();
        public string? BaseUrl { get; set; } = string.Empty;
        public IFormFile? BannerImg { get; set; }
        public List<Country> Countries { get; set; }
        public string BannerImgUrl { get; set; } = string.Empty;
    }
}
