using System.ComponentModel.DataAnnotations;

namespace vHolidays.Models.SupplyPartner.DataTransferObjects
{
    public class SupplierDestinationCreateModel
    {
        [Required(ErrorMessage = "Country is required.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "City(s) is required.")]
        //public string Cities { get; set; }
        public List<int> CityId { get; set; } = new();
    }
}
