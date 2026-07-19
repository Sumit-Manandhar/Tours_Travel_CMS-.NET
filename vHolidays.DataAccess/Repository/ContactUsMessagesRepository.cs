using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.DataAcess.Data;
using vHolidays.Models;

namespace vHolidays.DataAccess.Repository
{
    public class ContactUsMessagesRepository : Repository<ContactUsMessages>, IContactUsMessagesRepository
    {
        private readonly ApplicationDbContext _db;
        public ContactUsMessagesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void AddMessage(ContactUsMessages contactUsMessages)
        {
            _db.Add(contactUsMessages);
            _db.SaveChanges();
        }

    }
}
