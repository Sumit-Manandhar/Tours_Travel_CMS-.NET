using Travels.Models.Common;
using Travels.Models.Master.DatabaseModel;
using Travels.Models.Master.DataTransferObjects;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface ITourTypeRepository : IRepository<TourType>
    {
        Task<ResponseModel<int>> CreateUpdate(TourTypeDTO model);
        Task<ResponseModel<object>> OnOffDelete(int id);
    }
}
