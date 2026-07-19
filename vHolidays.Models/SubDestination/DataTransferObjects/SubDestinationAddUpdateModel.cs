using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vHolidays.Models.Base;
using vHolidays.Models.Regions;

namespace vHolidays.Models.SubDestination.DataTransferObjects
{
    public class SubDestinationAddUpdateModel : BaseEntity
    {
        public int DestinationId { get; set; }
        public int CountryId { get; set; }
        public string DestinationName { get; set; } = string.Empty;
        public string SubDestinationName { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string ShortDesc { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TopDestinations { get; set; } = string.Empty;
        public IFormFile? DisplayImg { get; set; }
        public IFormFile? BannerImg { get; set; }
        public List<IFormFile> Images { get; set; }
        public string DisplayImageUrl { get; set; }
        public string BannerImageUrl { get; set; }
        public List<City> Cities { get; set; }

        public List<string> ImageUrls { get; set; }
        public string? BaseUrl { get; set; } = string.Empty;

        public Dictionary<int,string> AdditionalImages { get; set; }
        public int[]? toDeleteImages { get; set; }
    }
}
