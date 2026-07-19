using Travels.DataAcess.Data;
using Travels.Models;
using Travels.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Travels.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db) {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }


        public async void Initialize()
        {
            //migrations if they are not applied
            try {
                if (_db.Database.GetPendingMigrations().Count() > 0) {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex) { }
            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult()) {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser {
                    UserName = "admin@vietjet.com",
                    Email = "admin@vietjet.com",
                    Name = "VietJet Holidays",
                    PhoneNumber = "6388 8883",
                    StreetAddress = "101 Kitchener Road #03-38 Jalan Besar Plaza, Singapore 208511",
                    State = "Singapore",
                    PostalCode = "208511",
                    City = "Singapore"
                }, "Admin123*").GetAwaiter().GetResult();
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@vietjet.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
