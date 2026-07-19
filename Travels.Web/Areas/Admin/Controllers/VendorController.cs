using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Repository.IRepository;
using Travels.Utility;

namespace TravelsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class VendorController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public VendorController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var vendors = await _unitOfWork.SupplierDetail.GetAll().ToListAsync();
            return View(vendors);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var vendorDetail = await _unitOfWork.SupplierDetail.Detail(Id);
            return View(vendorDetail);
        }
    }
}
