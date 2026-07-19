using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Interface
{
    public interface IPackageHotelRepository : IRepository<PackageHotel>
    {
        Task<ResponseModel<int>> CreateUpdate(PackageHotelViewModel model);
        Task<List<HotelPackageViewModel>> PackageHotel(int PackageDetailId);
        Task<ResponseModel<object>> DeleteHotelPackages(int HotelId,int packageId);
    }
}
