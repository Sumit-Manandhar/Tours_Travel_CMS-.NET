using Microsoft.AspNetCore.Http;
using vHolidays.Models.Regions;

namespace vHolidays.Models.Hotel
{
    public class HotelListViewModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string HotelName { get; set; }
        public string AddressLine { get; set; }
        public int CityId { get; set; }
        public string DisplayImageUrl { get; set; }
        public string BaseUrl { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public int? PackageId { get; set; }
        public IFormFile? Image { get; set; }
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
        public string RedirectUrl { get; set; }
    }
}
