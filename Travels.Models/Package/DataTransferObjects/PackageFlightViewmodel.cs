using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Models.Package.DataTransferObjects
{
    public class PackageFlightViewModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FlightNumber { get; set; }
        public string PNRNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturn { get; set; }
        public bool IsMultiCity { get; set; }
    }
}
