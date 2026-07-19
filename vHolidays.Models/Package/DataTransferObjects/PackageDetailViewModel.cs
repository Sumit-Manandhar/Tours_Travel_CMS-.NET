namespace vHolidays.Models.Package.DataTransferObjects
{
    public class PackageDetailViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Day { get; set; }
        public int Night { get; set; }
        public int MinAge { get; set; }
        public DateOnly Validity { get; set; }
        public decimal Rating { get; set; }
        public DateOnly? StartDate { get; set; }
        public bool IsGroupBooking { get; set; }

        public string PackageSummary { get; set; }
        public string TermsAndCondition { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string ImgUrl { get; set; }
        public List<PackageItineraryViewModel> Itinerary { get; set; } = new();
        public List<PackageInclusionsViewModel> Inclusion { get; set; } = new();
        public List<PackageTypesViewModel> PackageTypes { get; set; }
        public List<PackageFlightViewModel> Flights { get; set; }
        public List<HotelPackageViewModel> Hotel { get; set; } = new();
        public List<string> Images { get; set; }
        public PriceViewModel? Price { get; set; }
        public int? PreviousId { get; set; }
        public int? NextId { get; set; }
    }

}
