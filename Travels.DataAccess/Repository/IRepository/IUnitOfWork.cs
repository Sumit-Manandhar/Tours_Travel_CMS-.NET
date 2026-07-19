using Travels.DataAccess.Repository.Package.Interface;
using Travels.DataAccess.Repository.SupplyPartner.Interface;
using Travels.DataAccess.Repository.TailorTrip.Interface;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDestinationsRepository Destination { get; }
        ISubdestinationsRepository Subdestination { get; }
        IPackageDetailReposotory Package { get; }
        IPackageInclusionsReposotory PackageInclusion { get; }
        IPackageItineraryReposotory PackageItinerary { get; }
        IPackageImageRepository PackageImage { get; }
        IPackageGroupRepository PackageGroup { get; }
        IPackagePriceRepository PackagePrice { get; }
        IPackageClassOptionRepository PackageClassOption { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ICountryRepository Country { get; }
        IContactsRepository Contacts { get; }
        IContactUsMessagesRepository ContactUsMessages { get; }
        IContactBranchesRepository ContactBranches { get; }
        IBranchAgentsRepository BranchAgents { get; }
        ITourTypeRepository TourTypes { get; }
        IClassOptionrepository ClassOption { get; }
        IBookingRepository PackageBooking { get; }
        IHotelRepository Hotel { get; }
        IPackageHotelRepository PackageHotel { get; }
        ITailorTripRepository TailorTrip { get; }
        ISupplierDetailRepository SupplierDetail { get; }
        IPackageFlightRepository PackageFlight { get; }

        void Save();
    }
}
