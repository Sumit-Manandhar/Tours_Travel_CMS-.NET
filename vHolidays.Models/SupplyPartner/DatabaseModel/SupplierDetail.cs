using vHolidays.Models.Base;
using vHolidays.Models.Regions;

namespace vHolidays.Models.SupplyPartner.DatabaseModel
{
    public class SupplierDetail : BaseEntity
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;

        public int StateId { get; set; }
        public virtual State State { get; set; } = null!;

        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;

        public string Zip { get; set; }
        public string Currency { get; set; }

        public virtual List<SupplierDestination> Destination { get; set; }
        public virtual List<SupplierProducts> Product { get; set; }

        public string UpdatedTel { get; set; }
        public string UpdatedFax { get; set; }
        public string Website { get; set; }
        public string CentralEmail { get; set; }
        public string ReservationEmail1 { get; set; }
        public string ReservationEmail2 { get; set; }
        public string License { get; set; }
        public virtual List<SupplierContact> Contact { get; set; }
    }
}
