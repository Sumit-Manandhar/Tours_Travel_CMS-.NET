using System.ComponentModel.DataAnnotations;
using vHolidays.Models.Regions;

namespace vHolidays.Models.SupplyPartner.DatabaseModel
{
    public class SupplierDestination
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; } 
        public virtual Country Country { get; set; } = null!;

        public virtual List<SupplierDestinationCity> City { get; set; }

        public int SupplierDetailId { get; set; }
        public virtual SupplierDetail SupplierDetail { get; set; }
    }

    public class SupplierDestinationCity
    {
        [Key]
        public int Id { get; set; }
        public int CityId { get; set; } 
        public virtual City City { get; set; } = null!;

        public int SupplierDestinationId { get; set; }
        public virtual SupplierDestination SupplierDestination { get; set; }
    }
}
