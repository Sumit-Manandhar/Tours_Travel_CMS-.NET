using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Interface
{
    public interface IPackageItineraryReposotory : IRepository<PackageItinerary>
    {
        Task<ResponseModel<int>> CreateUpdate(List<ItineraryAddUpdateModel> items);
        Task<ItineraryAddUpdateModel> Get(int Id);
        Task<List<PackageItineraryViewModel>> Detail(int Id);
    }
}
