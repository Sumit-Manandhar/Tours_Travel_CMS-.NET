using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Interface
{
    public interface IPackageDetailReposotory : IRepository<PackageDetail>
    {
        Task<ResponseModel<int>> CreateUpdate(PackageDetailAddUpdateModel model);
        Task<PackageDetailAddUpdateModel> Detail(int Id);
        Task<List<PackageDetailViewModel>> GetList(bool isFeatured, bool isGroup);
        Task<PackageDetailViewModel> PackageDetail(int Id);
        Task<ResponseModel<int>> SyncToGroupBooking(SyncPackageDTO model);
        Task<List<PackageDetailViewModel>> GetListByCountry(bool isFeatured, bool isGroup, int countryId);


    }
}
