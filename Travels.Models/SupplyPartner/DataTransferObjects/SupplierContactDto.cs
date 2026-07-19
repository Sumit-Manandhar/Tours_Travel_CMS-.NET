using Travels.Utility.Enumerations;

namespace Travels.Models.SupplyPartner.DataTransferObjects
{
    public class SupplierContactDto
    {
        public int Id { get; set; }
        public VendorContactEnum Type { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string DirectNo { get; set; }
        public string Mobile { get; set; }

    }
}
