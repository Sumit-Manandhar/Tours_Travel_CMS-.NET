using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vHolidays.Models.Package.DataTransferObjects
{
    public class PackageFlightAddUpdateModel
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FlightNumber { get; set; }
        public string PNRNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturn { get; set; }
        public bool IsMultiCity { get; set; }

        public int PackageDetailId { get; set; }
    }

    public class SaveFlightModel
    {
        public int PackageDetailId { get; set; }
        public List<PackageFlightAddUpdateModel> Flights { get; set; }
    }
}
