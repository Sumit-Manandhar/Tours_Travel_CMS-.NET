using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Interface
{
    public interface IPackagePriceRepository : IRepository<PackagePrice>
    {
        Task<PriceViewModel> GetAll(int Id, bool AllPrice);
        Task<ResponseModel<string>> Update(List<PackagePriceDto> model);
    }
}
