using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vHolidays.Models.Base;

namespace vHolidays.Models.Package.DatabaseModel
{
    public class PackageFlight : BaseEntity
    {

        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FlightNumber { get; set; }
        public string PNRNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturn { get; set; }
        public bool IsMultiCity { get; set; }

        public int PackageDetailId { get; set; }
        public virtual PackageDetail PackageDetail { get; set; } = null!;

    }
}
