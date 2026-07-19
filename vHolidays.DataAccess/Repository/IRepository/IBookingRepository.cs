using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.IRepository
{
    public interface IBookingRepository : IRepository<PackageBooking>
    {
        Task<ResponseModel<int>> BookNow(PackageBookingDTO model);
        Task<PackageBookingViewModel> GetDetail(int Id);
    }
}
