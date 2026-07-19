using System.ComponentModel.DataAnnotations;
using vHolidays.Models.Regions;

namespace vHolidays.Models.TailorTrip.DataTransferObjects
{
    public class TailorTripCreateModel
    {
        [Required(ErrorMessage = "Travel Date is required.")]
        public DateOnly TravelDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required(ErrorMessage = "Length of trave, is required.")]
        public int Length { get; set; } = 1;
        [Required(ErrorMessage = "Hotel category is required.")]
        public string HotelCategory { get; set; }
        [Required(ErrorMessage = "Activity is required.")]
        public string Activity { get; set; }
        [Required(ErrorMessage = "No of Adult is required.")]
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        [Required(ErrorMessage = "Destination is required.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }
        public List<Country> Countries { get; set; }
        public TailorTripPersonalDetailCreateModel PersonalDetail { get; set; } = new();
        public string ContactPhoneCode { get; set; }
    }
}
