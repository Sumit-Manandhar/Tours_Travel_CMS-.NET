using vHolidays.Models;

namespace vHolidays.DataAccess.Repository.IRepository
{
    public interface IContactUsMessagesRepository :IRepository<ContactUsMessages>
    {
        void AddMessage(ContactUsMessages contactUsMessages);
    }
}
