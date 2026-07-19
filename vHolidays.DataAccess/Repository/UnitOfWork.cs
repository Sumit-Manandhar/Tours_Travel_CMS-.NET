using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.DataAccess.Repository.Package.Interface;
using vHolidays.DataAccess.Repository.Package.Repository;
using vHolidays.DataAccess.Repository.SupplyPartner.Interface;
using vHolidays.DataAccess.Repository.SupplyPartner.Repository;
using vHolidays.DataAccess.Repository.TailorTrip.Interface;
using vHolidays.DataAccess.Repository.TailorTrip.Repository;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.SupplyPartner.DatabaseModel;

namespace vHolidays.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IDestinationsRepository Destination { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IContactsRepository Contacts { get; private set; }
        public IContactBranchesRepository ContactBranches { get; private set; }
        public IContactUsMessagesRepository ContactUsMessages { get; private set; }
        public ISubdestinationsRepository Subdestination { get; private set; }
        public IPackageDetailReposotory Package { get; private set; }
        public IPackageInclusionsReposotory PackageInclusion { get; private set; }
        public IPackageItineraryReposotory PackageItinerary { get; private set; }
        public IPackageClassOptionRepository PackageClassOption { get; private set; }
        public IPackageGroupRepository PackageGroup { get; private set; }
        public IPackagePriceRepository PackagePrice { get; private set; }
        public IPackageImageRepository PackageImage { get; private set; }
        public IBranchAgentsRepository BranchAgents { get; private set; }
        public ITourTypeRepository TourTypes { get; private set; }
        public IClassOptionrepository ClassOption { get; private set; }
        public IBookingRepository PackageBooking { get; private set; }
        public IHotelRepository Hotel { get; private set; }
        public IPackageHotelRepository PackageHotel { get; private set; }
        public ITailorTripRepository TailorTrip { get; private set; }
        public ISupplierDetailRepository SupplierDetail { get; private set; }
        public IPackageFlightRepository PackageFlight  { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Destination = new DestinationsRepository(_db);
            Subdestination = new SubdestinationsRepository(_db);
            Country = new CountryRepository(_db);
            Contacts = new ContactsRepository(_db);
            ContactBranches = new ContactBranchesRepository(_db);
            ContactUsMessages = new ContactUsMessagesRepository(_db);
            Package = new PackageDetailReposotory(_db);
            PackageInclusion = new PackageInclusionsReposotory(_db);
            PackageItinerary = new PackageItineraryReposotory(_db);
            PackageGroup = new PackageGroupRepository(_db);
            PackageClassOption = new PackageClassOptionRepository(_db);
            PackagePrice = new PackagePriceRepository(_db);
            PackageImage = new PackageImageRepository(_db);
            BranchAgents = new BranchAgentsRepository(_db);
            TourTypes = new TourTypeRepository(_db);
            ClassOption = new ClassOptionRepository(_db);
            PackageBooking = new BookingRepository(_db);
            Hotel = new HotelRepository(_db);
            PackageHotel = new PackageHotelRepository(_db);
            TailorTrip = new TailorTripRepository(_db);
            SupplierDetail = new SupplierDetailRepository(_db);
            PackageFlight = new PackageFlightRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
