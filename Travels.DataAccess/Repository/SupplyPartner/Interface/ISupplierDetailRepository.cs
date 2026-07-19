using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.SupplyPartner.DatabaseModel;
using Travels.Models.SupplyPartner.DataTransferObjects;

namespace Travels.DataAccess.Repository.SupplyPartner.Interface
{
    public interface ISupplierDetailRepository : IRepository<SupplierDetail>
    {
        Task<ResponseModel<string>> Create(SupplierDetailCreateModel model);
        Task<SupplierDetailDto> Detail(int id);
    }
}
