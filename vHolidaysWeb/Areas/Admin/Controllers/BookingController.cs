using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Utility;

namespace vHolidaysWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BookingController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public BookingController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _unitOfWork.PackageBooking.GetAll().ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await _unitOfWork.PackageBooking.GetDetail(id);
            return View(data);
        }
    }
}
