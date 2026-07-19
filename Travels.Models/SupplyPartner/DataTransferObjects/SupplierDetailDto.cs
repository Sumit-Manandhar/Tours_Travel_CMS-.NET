namespace Travels.Models.SupplyPartner.DataTransferObjects
{
    public class SupplierDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public int CountryId { get; set; }
        public string Country { get; set; } = null!;

        public int StateId { get; set; }
        public string State { get; set; } = null!;

        public int CityId { get; set; }
        public string City { get; set; } = null!;

        public string Zip { get; set; }
        public string Currency { get; set; }
        public string UpdatedTel { get; set; }
        public string UpdatedFax { get; set; }
        public string Website { get; set; }
        public string CentralEmail { get; set; }
        public string ReservationEmail1 { get; set; }
        public string ReservationEmail2 { get; set; }
        public string License { get; set; }


        public List<SupplierDestinationDto> Destination { get; set; }
        public List<SupplierProductsDto> Product { get; set; }
        public List<SupplierContactDto> Contact { get; set; }
    }
}
