using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Travels.DataAccess.Repository.IRepository;
using Travels.Models;
using Travels.Models.Package.DataTransferObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelsWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public PackageController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            PackageListModel data = new();
            data.IsGroupBooking = false;
            data.DatList = await _unitOfWork.Package.GetList(false, data.IsGroupBooking);
            return View(data);
        }

        public async Task<IActionResult> Group()
        {
            PackageListModel data = new();
            data.IsGroupBooking = true;
            data.DatList = await _unitOfWork.Package.GetList(false, data.IsGroupBooking);
            return View("~/Areas/Customer/Views/Package/Index.cshtml", data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var data = await _unitOfWork.Package.PackageDetail(Id);

            var isGroupBooking = false;
            if (data.IsGroupBooking)
            {
                isGroupBooking = true;
            }
            var package = await _unitOfWork.Package.GetListByCountry(false, isGroupBooking, data.CountryId);
            var orderedIds = package.OrderBy(a => a.Id).Select(a => a.Id).ToArray();

            var currentPosition = Array.IndexOf(orderedIds, data.Id);
            if (currentPosition > 0)
            {
                data.PreviousId = orderedIds[currentPosition - 1];
            }
            if (currentPosition >= 0 && currentPosition < orderedIds.Length - 1)
            {
                data.NextId = orderedIds[currentPosition + 1];
            }
            return View(data);
        }

        public async Task<IActionResult> Inclusion(int Id)
        {
            var data = await _unitOfWork.PackageInclusion.Detail(Id);
            return PartialView("_partialPackageInclusion", data);
        }

        public async Task<IActionResult> Itinerary(int Id)
        {
            var data = await _unitOfWork.PackageItinerary.Detail(Id);
            return PartialView("_partialPackageItinerary", data);
        }
        
        public async Task<IActionResult> Hotel(int Id)
        {
            var data = await _unitOfWork.PackageHotel.PackageHotel(Id);
            return PartialView("_partialPackageHotel", data);
        }

        public async Task<IActionResult> Price(int Id)
        {
            var data = await _unitOfWork.PackagePrice.GetAll(Id, false);
            return PartialView("_partialPackagePrice", data);
        }

        public async Task<IActionResult> BookNow(int id)
        {
            var model = new PackageBookingDTO();
            model.PackageId = id;
            var package = _unitOfWork.Package.Get(a => a.Id == id);
            DateTime? startDate = package.StartDate.HasValue ? package.StartDate.Value.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null;
            model.DepartureDate = startDate.HasValue ? startDate.Value : DateTime.Now;
            model.IsGroupBooking = package.IsGroupBooking;
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitBooking(PackageBookingDTO model)
        {
            var response = await _unitOfWork.PackageBooking.BookNow(model);
            return Json(response);
        }

        public async Task<IActionResult> TermsNCond()
        {
            return View();
        }

    }
}
