using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.SupplyPartner.DatabaseModel;
using vHolidays.Models.SupplyPartner.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.SupplyPartner.Interface
{
    public interface ISupplierDetailRepository : IRepository<SupplierDetail>
    {
        Task<ResponseModel<string>> Create(SupplierDetailCreateModel model);
        Task<SupplierDetailDto> Detail(int id);
    }
}
