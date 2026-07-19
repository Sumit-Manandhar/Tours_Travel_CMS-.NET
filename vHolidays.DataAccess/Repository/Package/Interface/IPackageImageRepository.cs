using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;

namespace vHolidays.DataAccess.Repository.Package.Interface
{
    public interface IPackageImageRepository : IRepository<PackageImage>
    {
        Task<ResponseModel<string>> SaveImages(int Id, List<string> urls, int[]? todelete);
    }
}
