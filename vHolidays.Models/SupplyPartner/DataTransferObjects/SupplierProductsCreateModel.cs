namespace vHolidays.Models.SupplyPartner.DataTransferObjects
{
    public class SupplierProductsCreateModel
    {
        public int ProductId { get; set; } // Probably need to configure master data--> for now enum value
        public string SupplierName { get; set; }
    }
}
