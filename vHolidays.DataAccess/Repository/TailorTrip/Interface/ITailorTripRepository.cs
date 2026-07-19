using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.TailorTrip.DatabaseModel;
using vHolidays.Models.TailorTrip.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.TailorTrip.Interface
{
    public interface ITailorTripRepository : IRepository<TailorMadeTrip>
    {
        Task<ResponseModel<string>> Create(TailorTripCreateModel model);
        Task<TailorTripDto> Detail(int id);
    }
}
