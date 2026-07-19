using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Interface
{
    public interface IPackageGroupRepository : IRepository<PackageGroup>
    {
        Task<ResponseModel<int>> CreateUpdate(GroupAddUpdateModel model);
        Task<GroupAddUpdateModel> Get(int Id);
        Task<ResponseModel<string>> UpdateOrder(List<DataOrderingModel> model);
    }
}
