using Microsoft.AspNetCore.Mvc;
using vHolidays.DataAccess.Repository.Common.Interface;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.SupplyPartner.DataTransferObjects;
using vHolidays.Utility.Enumerations;

namespace vHolidaysWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class VendorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilityService _utilities;

        public VendorController(IUnitOfWork unitOfWork, IUtilityService utilities)
        {
            _unitOfWork = unitOfWork;
            _utilities = utilities;
        }

        public async Task<IActionResult> Index()
        {
            SupplierDetailCreateModel model = new();
            model.Destination.Add(new SupplierDestinationCreateModel() { CityId = new List<int>() });
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            model.CurrencyList = await _unitOfWork.Country.GetCountriesCurrency();
            model.Contact = Enum.GetValues(typeof(VendorContactEnum)).Cast<VendorContactEnum>().Select(_ => new SupplierContactCreateModel()).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierDetailCreateModel model)
        {
            model.Product = model.ProductIds.Select( x => new SupplierProductsCreateModel()
            {
                ProductId = x,
                SupplierName = _utilities.GetProductSuppliesName(x),
            }).ToList();
            var response = await _unitOfWork.SupplierDetail.Create(model);
            return Json(response);
        }
    }
}
