using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Repository.IRepository;
using Travels.Utility;

namespace TravelsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class TailorTripController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public TailorTripController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var trips = await _unitOfWork.TailorTrip.GetAll().ToListAsync();
            return View(trips);
        }
        public async Task<IActionResult> Details(int Id)
        {
            var tripDetail = await _unitOfWork.TailorTrip.Detail(Id);
            return View(tripDetail);
        }
    }
}
