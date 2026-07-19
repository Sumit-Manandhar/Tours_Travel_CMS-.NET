using vHolidays.Models.Common;
using vHolidays.Models.Master.DatabaseModel;
using vHolidays.Models.Master.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.IRepository
{
    public interface IClassOptionrepository : IRepository<ClassOption>
    {
        Task<ResponseModel<int>> CreateUpdate(ClassOptionDto model);
        Task<ResponseModel<object>> OnOffDelete(int id);
    }
}
