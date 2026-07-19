using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Interface
{
    public interface IPackagePriceRepository : IRepository<PackagePrice>
    {
        Task<PriceViewModel> GetAll(int Id, bool AllPrice);
        Task<ResponseModel<string>> Update(List<PackagePriceDto> model);
    }
}
