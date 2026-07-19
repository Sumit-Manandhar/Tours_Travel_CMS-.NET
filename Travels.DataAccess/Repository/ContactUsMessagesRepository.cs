using Travels.DataAccess.Repository.IRepository;
using Travels.DataAcess.Data;
using Travels.Models;

namespace Travels.DataAccess.Repository
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
