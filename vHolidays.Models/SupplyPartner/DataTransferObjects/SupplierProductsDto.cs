namespace vHolidays.Models.SupplyPartner.DataTransferObjects
{
    public class SupplierProductsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Probably need to configure master data--> for now enum value
        public string SupplierName { get; set; }
    }
}
