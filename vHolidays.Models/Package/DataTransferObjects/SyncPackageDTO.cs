using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vHolidays.Models.Package.DataTransferObjects
{
    public class SyncPackageDTO
    {
        public int Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool CopyInclusion { get; set; }
        public bool CopyItinerary { get; set; }
        public bool CopyHotel { get; set; }
        public bool CopyGroup { get; set; }
        public bool CopyFlight { get; set; }
        public bool CopyImages { get; set; }
    }
}
