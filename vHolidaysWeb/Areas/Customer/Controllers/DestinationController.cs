using Microsoft.AspNetCore.Mvc;
using vHolidays.DataAccess.Repository.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vHolidaysWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class DestinationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public DestinationController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> GetDetails(int id )
        {
            var destination = _unitOfWork.Destination.GetById(id);
            destination.Hotels= _unitOfWork.Hotel.GetActiveten(destination.CountryId);
            destination.Packages = await _unitOfWork.Package.GetListByCountry(false,false, destination.CountryId);

            return View(destination);
        }

        public IActionResult Detail(int Id)
        {
            var model = _unitOfWork.Subdestination.GetById(Id);
            return View(model);
        }
    }
}
