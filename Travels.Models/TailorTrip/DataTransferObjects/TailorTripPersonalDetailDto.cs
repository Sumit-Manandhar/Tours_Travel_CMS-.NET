namespace Travels.Models.TailorTrip.DataTransferObjects
{
    public class TailorTripPersonalDetailDto
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SpecialRequest { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
    }
}
