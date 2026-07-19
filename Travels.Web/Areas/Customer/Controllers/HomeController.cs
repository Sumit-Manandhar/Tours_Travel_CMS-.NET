using Microsoft.AspNetCore.Mvc;
using Travels.DataAccess.Repository.IRepository;
using Travels.Models;
using Travels.Models.Common;
using Travels.Models.ViewModels.Home;

namespace Travels.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new();
            model.PackageList = await _unitOfWork.Package.GetList(true, false);
            return View(model);
        }

        public async Task<IActionResult> Contact()
        {
            var scheme = Request.Scheme;
            var host = Request.Host.Value;
            ViewBag.BaseUrl = $"{scheme}://{host}//";
            var data = await _unitOfWork.ContactBranches.GetAll();
            return View(data.Where(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactUsMessages model)
        {
            _unitOfWork.ContactUsMessages.AddMessage(model);
            return Json(new ResponseModel<object> { Message = "Message Sent", Succeeded = true });
        }

    }
}