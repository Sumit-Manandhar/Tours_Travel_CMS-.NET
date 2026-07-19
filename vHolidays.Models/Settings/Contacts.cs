using vHolidays.Models.Base;

namespace vHolidays.Models.Settings
{
    public class Contacts : BaseEntity
    {
        public string? ContactNumber { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactAddress { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? MapLinks { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? YoutubeLink { get; set; }

    }
}
