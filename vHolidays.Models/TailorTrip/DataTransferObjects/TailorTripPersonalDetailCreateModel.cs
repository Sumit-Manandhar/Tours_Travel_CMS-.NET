using System.ComponentModel.DataAnnotations;

namespace vHolidays.Models.TailorTrip.DataTransferObjects
{
    public class TailorTripPersonalDetailCreateModel
    {
        [Required]
        public string Salutation { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "last Name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage ="Invalid Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact Number is required.")]
        public string PhoneNumber { get; set; }
        public string SpecialRequest { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
