using vHolidays.Models.Package.DatabaseModel;

namespace vHolidays.Models.Package.DataTransferObjects
{
    public class PackageHotelViewModel
    {
        public int PackageId { get; set; }
        public int[] HotelId { get; set; }
        public int[] ToRemoveHotelIds { get; set; }
        public int CountryId { get; set; }
        public List<PackageHotel> Data { get; set; }
        public List<vHolidays.Models.Hotel.Hotel> Hotels { get; set; }
    }

}
