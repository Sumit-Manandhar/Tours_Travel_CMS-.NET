using vHolidays.Models.Settings;
using vHolidays.Models.Settings.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.IRepository
{
    public interface IContactsRepository :IRepository<Contacts>
    {
        Task Update(SaveContactsdto contact);
    }
}
