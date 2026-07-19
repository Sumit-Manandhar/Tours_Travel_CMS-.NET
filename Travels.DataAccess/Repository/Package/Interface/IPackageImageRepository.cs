using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;

namespace Travels.DataAccess.Repository.Package.Interface
{
    public interface IPackageImageRepository : IRepository<PackageImage>
    {
        Task<ResponseModel<string>> SaveImages(int Id, List<string> urls, int[]? todelete);
    }
}
