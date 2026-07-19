using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Interface
{
    public interface IPackageItineraryReposotory : IRepository<PackageItinerary>
    {
        Task<ResponseModel<int>> CreateUpdate(List<ItineraryAddUpdateModel> items);
        Task<ItineraryAddUpdateModel> Get(int Id);
        Task<List<PackageItineraryViewModel>> Detail(int Id);
    }
}
