using Microsoft.AspNetCore.Http;
using Travels.Models.Package.DataTransferObjects;
using Travels.Models.Regions;

namespace Travels.Models.ViewModels
{
    public class DestinationViewModel
    {
        public List<Country> Countries { get; set; }
        public DestinationDetailsModel Data { get; set; }
    }

    public class SaveDestinationModel
    {
        public int Id { get; set; }
        public string DestinationName { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string ShortDesc { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public string Overview { get; set; } = string.Empty;
        public IFormFile? DisplayImg { get; set; }
        public IFormFile? BannerImg { get; set; }
        public List<IFormFile> Images { get; set; }
        public string DisplayImageUrl { get; set; }
        public string BannerImageUrl { get; set; }

        public List<string> ImageUrls { get; set; }

    }
    public class DestinationDetailsModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public List<ImageViewModel> Images { get; set; } = new();
        public string BaseUrl { get; set; } = string.Empty;
        public List<SubDestinationListViewModel>? SubDestinations { get; set; } = new();

        public List<Travels.Models.Hotel.Hotel>? Hotels { get; set; } = new();
        public List<PackageDetailViewModel>? Packages { get; set; } = new();
    }
    public class ImageViewModel
    {
        public int Id { get; set; } = 0;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public bool IsBanner { get; set; }
    }
    public class SubDestinationListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
    }
}
