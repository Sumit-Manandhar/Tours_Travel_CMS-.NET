namespace vHolidays.Models.SupplyPartner.DataTransferObjects
{
    public class SupplierDestinationDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; } = null!;
        public List<SupplierDestinationCityDto> City { get; set; }
    }
    public class SupplierDestinationCityDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string City { get; set; } = null!;
    }
}
