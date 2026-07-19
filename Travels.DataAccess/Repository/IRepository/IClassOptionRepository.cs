using Travels.Models.Common;
using Travels.Models.Master.DatabaseModel;
using Travels.Models.Master.DataTransferObjects;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface IClassOptionrepository : IRepository<ClassOption>
    {
        Task<ResponseModel<int>> CreateUpdate(ClassOptionDto model);
        Task<ResponseModel<object>> OnOffDelete(int id);
    }
}
