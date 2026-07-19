using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Travels.Models.Regions;

namespace Travels.Models.SupplyPartner.DataTransferObjects
{
    public class SupplierDetailCreateModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address1 is required.")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "Address2 is required.")]
        public string Address2 { get; set; }

        public int CountryId { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Zip is required.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Telephone is required.")]
        public string UpdatedTel { get; set; }

        [Required(ErrorMessage = "Fax is required.")]
        public string UpdatedFax { get; set; }

        [Required(ErrorMessage = "Website is required.")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Central Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string CentralEmail { get; set; }

        [Required(ErrorMessage = "Reservation Email1 is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string ReservationEmail1 { get; set; }

        [Required(ErrorMessage = "Reservation Email2 is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string ReservationEmail2 { get; set; }
        [Required(ErrorMessage = "License is required.")]
        public string License { get; set; }
        public List<int> ProductIds { get; set; } = new();
        public List<SupplierDestinationCreateModel> Destination { get; set; } = new();
        public List<SupplierProductsCreateModel> Product { get; set; } = new();
        public virtual List<SupplierContactCreateModel> Contact { get; set; } = new();

        public List<Country>? Countries { get; set; }
        public List<SelectListItem>? CurrencyList { get; set; }
    }
}
