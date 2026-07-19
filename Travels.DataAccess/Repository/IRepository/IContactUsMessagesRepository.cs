using Travels.Models;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface IContactUsMessagesRepository :IRepository<ContactUsMessages>
    {
        void AddMessage(ContactUsMessages contactUsMessages);
    }
}
