using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travels.Utility;

namespace TravelsWeb.Areas.Admin.Controllers
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
