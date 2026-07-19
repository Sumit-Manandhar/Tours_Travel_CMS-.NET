using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface IBookingRepository : IRepository<PackageBooking>
    {
        Task<ResponseModel<int>> BookNow(PackageBookingDTO model);
        Task<PackageBookingViewModel> GetDetail(int Id);
    }
}
