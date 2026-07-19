using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.TailorTrip.DatabaseModel;
using Travels.Models.TailorTrip.DataTransferObjects;

namespace Travels.DataAccess.Repository.TailorTrip.Interface
{
    public interface ITailorTripRepository : IRepository<TailorMadeTrip>
    {
        Task<ResponseModel<string>> Create(TailorTripCreateModel model);
        Task<TailorTripDto> Detail(int id);
    }
}
