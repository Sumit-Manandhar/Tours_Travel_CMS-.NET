using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Interface
{
    public interface IPackageGroupRepository : IRepository<PackageGroup>
    {
        Task<ResponseModel<int>> CreateUpdate(GroupAddUpdateModel model);
        Task<GroupAddUpdateModel> Get(int Id);
        Task<ResponseModel<string>> UpdateOrder(List<DataOrderingModel> model);
    }
}
