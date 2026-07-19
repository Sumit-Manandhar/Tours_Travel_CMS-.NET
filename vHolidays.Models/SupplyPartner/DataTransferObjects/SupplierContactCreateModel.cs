using System.ComponentModel.DataAnnotations;
using vHolidays.Utility.Enumerations;

namespace vHolidays.Models.SupplyPartner.DataTransferObjects
{
    public class SupplierContactCreateModel
    {
        public VendorContactEnum Type { get; set; }
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Direct No is required.")]
        public string DirectNo { get; set; }
        [Required(ErrorMessage = "Mobile is required.")]
        public string Mobile { get; set; }
    }
}
