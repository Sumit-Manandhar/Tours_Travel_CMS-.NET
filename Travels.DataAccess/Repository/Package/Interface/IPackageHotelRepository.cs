using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Interface
{
    public interface IPackageHotelRepository : IRepository<PackageHotel>
    {
        Task<ResponseModel<int>> CreateUpdate(PackageHotelViewModel model);
        Task<List<HotelPackageViewModel>> PackageHotel(int PackageDetailId);
        Task<ResponseModel<object>> DeleteHotelPackages(int HotelId,int packageId);
    }
}
