using Travels.Models.Settings;
using Travels.Models.Settings.DataTransferObjects;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface IContactsRepository :IRepository<Contacts>
    {
        Task Update(SaveContactsdto contact);
    }
}
