using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Interface
{
    public interface IPackageInclusionsReposotory : IRepository<PackageInclusions>
    {
        Task<ResponseModel<int>> CreateUpdate(List<InclusionAddUpdateModel> model);
        Task<InclusionAddUpdateModel> Get(int Id);
        Task<List<PackageInclusions>> Detail(int Id);
    }
}
