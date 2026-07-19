using Travels.Models.Base;
using Travels.Utility.Enumerations;

namespace Travels.Models.SupplyPartner.DatabaseModel
{
    public class SupplierContact : BaseEntity
    {
        public VendorContactEnum Type { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string DirectNo { get; set; }
        public string Mobile { get; set; }

        public int SupplierDetailId { get; set; }
        public virtual SupplierDetail SupplierDetail { get; set; }
    }
}
