using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Interface
{
    public interface IPackageInclusionsReposotory : IRepository<PackageInclusions>
    {
        Task<ResponseModel<int>> CreateUpdate(List<InclusionAddUpdateModel> model);
        Task<InclusionAddUpdateModel> Get(int Id);
        Task<List<PackageInclusions>> Detail(int Id);
    }
}
