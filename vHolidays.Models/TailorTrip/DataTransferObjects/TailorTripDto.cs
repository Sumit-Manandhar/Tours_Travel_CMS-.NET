namespace vHolidays.Models.TailorTrip.DataTransferObjects
{
    public class TailorTripDto
    {
        public DateOnly TravelDate { get; set; }
        public int Length { get; set; }
        public string HotelCategory { get; set; }
        public string Activity { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int? CityId { get; set; }
        public string City { get; set; }
        public TailorTripPersonalDetailDto PersonalDetail { get; set; } = new();
    }
}
