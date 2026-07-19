using vHolidays.Models.Common;
using vHolidays.Models.Master.DatabaseModel;
using vHolidays.Models.Master.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.IRepository
{
    public interface ITourTypeRepository : IRepository<TourType>
    {
        Task<ResponseModel<int>> CreateUpdate(TourTypeDTO model);
        Task<ResponseModel<object>> OnOffDelete(int id);
    }
}
