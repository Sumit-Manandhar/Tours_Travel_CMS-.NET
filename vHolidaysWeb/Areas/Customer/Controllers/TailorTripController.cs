using Microsoft.AspNetCore.Mvc;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.TailorTrip.DataTransferObjects;

namespace vHolidaysWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class TailorTripController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TailorTripController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            TailorTripCreateModel model = new();
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            return View(model);
        }

        public async Task<IActionResult> Create(TailorTripCreateModel model)
        {
            if (!string.IsNullOrEmpty(model.ContactPhoneCode))
            {
                model.PersonalDetail.PhoneNumber = model.ContactPhoneCode + "-" + model.PersonalDetail.PhoneNumber;
            }
            var response = await _unitOfWork.TailorTrip.Create(model);
            return Json(response);
        }

    }
}
