using Microsoft.AspNetCore.Http;
using Travels.Models.Regions;

namespace Travels.Models.ViewModels
{
    public class SubdestinationViewModel
    {
        public List<City> Cities { get; set; }
        public SubdestinationDetailsModel Data { get; set; }
        public string DestinationName { get; set; }
        public int DestinationId { get; set; }
    }
    public class SaveSubdestinationModel
    {
        public int Id { get; set; }
        public int DestinationId { get; set; }

        public string SubdestinationName { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string ShortDesc { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public string TopDestination { get; set; } = string.Empty;
        public IFormFile? DisplayImg { get; set; }
        public IFormFile? BannerImg { get; set; }
        public List<IFormFile> Images { get; set; }
        public string DisplayImageUrl { get; set; }
        public string BannerImageUrl { get; set; }

        public List<string> ImageUrls { get; set; }
        public int[]? toDeleteImages { get; set; }


    }
    public class SubdestinationDetailsModel
    {
        public int Id { get; set; }
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cityname { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TopDestination { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public List<ImageViewModel> Images { get; set; } = new();
        public string BaseUrl { get; set; } = string.Empty;
        public List<SubDestinationListViewModel>? OtherDestinations { get; set; } = new();

    }

}
