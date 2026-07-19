using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Settings;
using vHolidays.Models.Settings.DataTransferObjects;

namespace vHolidays.DataAccess.Repository
{
    public class ContactsRepository : Repository<Contacts>, IContactsRepository
    {
        private readonly ApplicationDbContext _db;
        public ContactsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task Update(SaveContactsdto obj)
        {
            var entity = _db.Contacts.FirstOrDefault();
            
            if (entity is null)
            {
                var contact = new Contacts()
                {
                    ContactNumber = obj.ContactNumber,
                    ContactEmail = obj.ContactEmail,
                    ContactAddress = obj.ContactAddress,
                    Latitude = obj.Latitude,
                    Longitude = obj.Longitude,
                    MapLinks = obj.MapLinks,
                    FacebookLink = obj.FacebookLink,
                    InstagramLink = obj.InstagramLink,
                    TwitterLink = obj.TwitterLink,
                    YoutubeLink = obj.YoutubeLink,
                    IsActive = obj.IsActive,
                    IsPublished = obj.IsPublished,
                    IsDeleted = obj.IsDeleted,
                };
                contact.CreatedBy = obj.UserName;
                contact.CreatedDate = DateTime.UtcNow;
               await _db.AddAsync(contact);
            }
            else
            {
                entity.ContactNumber = obj.ContactNumber;
                entity.ContactEmail = obj.ContactEmail;
                entity.ContactAddress = obj.ContactAddress;
                entity.Latitude = obj.Latitude;
                entity.Longitude = obj.Longitude;
                entity.MapLinks = obj.MapLinks;
                entity.FacebookLink = obj.FacebookLink;
                entity.InstagramLink = obj.InstagramLink;
                entity.TwitterLink = obj.TwitterLink;
                entity.YoutubeLink = obj.YoutubeLink;
                entity.IsActive = obj.IsActive;
                entity.IsPublished = obj.IsPublished;
                entity.IsDeleted = obj.IsDeleted;
                entity.ModifiedBy = obj.UserName;
                entity.ModifiedDate = DateTime.UtcNow;
                _db.Update(entity);
            }
        
            await _db.SaveChangesAsync();

        }

    }
}
