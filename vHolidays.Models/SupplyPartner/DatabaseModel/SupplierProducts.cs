using System.ComponentModel.DataAnnotations;

namespace vHolidays.Models.SupplyPartner.DatabaseModel
{
    public class SupplierProducts
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; } // Probably need to configure master data--> for now enum value
        public string SupplierName { get; set; }

        public int SupplierDetailId { get; set; }
        public virtual SupplierDetail SupplierDetail { get; set; }
    }
}
