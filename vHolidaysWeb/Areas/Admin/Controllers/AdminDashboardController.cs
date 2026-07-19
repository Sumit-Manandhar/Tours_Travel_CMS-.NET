using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vHolidays.Utility;

namespace vHolidaysWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class AdminDashboardController : Controller
    {
       
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
