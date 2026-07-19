using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Package.DataTransferObjects;
using Travels.Utility;

namespace TravelsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GroupBookingController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public GroupBookingController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index(string? keyword = "")
        {
            ViewData["IsGroupBooking"] = true;
            var Packages = _unitOfWork.Package.GetAll(a => a.IsGroupBooking && a.Name.ToLower().Contains(keyword ?? "".ToLower())).ToList();
            return View("~/Areas/Admin/Views/Package/Index.cshtml", Packages);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PackageDetailAddUpdateModel model = new();
            model.Validity = DateOnly.FromDateTime(DateTime.Now);
            model.CountryId = 1;
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            model.IsPublished = true;
            model.IsActive = true;
            model.TourTypes = _unitOfWork.TourTypes.GetAll(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList();
            model.ClassOptionTypes = _unitOfWork.ClassOption.GetAll(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList();
            return View("~/Areas/Admin/Views/Package/Create.cshtml", model);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            ViewData["IsGroupBooking"] = true;
            var model = await _unitOfWork.Package.Detail(Id);
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            model.BaseUrl = $"{Request.Scheme}://{Request.Host.Value}//";
            model.TourTypes = _unitOfWork.TourTypes.GetAll(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList();
            model.ClassOptionTypes = _unitOfWork.ClassOption.GetAll(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList();
            return View("~/Areas/Admin/Views/Package/Edit.cshtml", model);
        }

        public async Task<IActionResult> Inclusion(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = true;
            var model = await _unitOfWork.PackageInclusion.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View("~/Areas/Admin/Views/Package/Inclusion.cshtml", model);
        }

        public async Task<IActionResult> Itinerary(int Id)
        {
            ViewData["IsGroupBooking"] = true;
            ViewData["PackageDetailId"] = Id;
            var model = await _unitOfWork.PackageItinerary.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View("~/Areas/Admin/Views/Package/Itinerary.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Images(int Id)
        {
            ViewData["IsGroupBooking"] = true;
            ViewData["PackageDetailId"] = Id;
            var model = await _unitOfWork.PackageImage.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View("~/Areas/Admin/Views/Package/Images.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Hotels(int Id)
        {
            ViewData["IsGroupBooking"] = true;
            ViewData["PackageDetailId"] = Id;
            var package = _unitOfWork.Package.Get(a => a.Id == Id);
            var data = await _unitOfWork.PackageHotel.GetAll(x => x.PackageId == Id).ToListAsync();
            var model = new PackageHotelViewModel()
            {
                Data = data,
                PackageId = Id,
                Hotels = _unitOfWork.Hotel.GetActiveten(package.CountryId),
                CountryId = package.CountryId
            };
            return View("~/Areas/Admin/Views/Package/Hotels.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> PackageGroup(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = true;
            var model = await _unitOfWork.PackageGroup.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View("~/Areas/Admin/Views/Package/PackageGroup.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Price(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = true;
            var model = await _unitOfWork.PackagePrice.GetAll(Id, true);
            return View("~/Areas/Admin/Views/Package/PackagePrice.cshtml", model);
        }
        [HttpGet]
        public async Task<IActionResult> AddUpdateItinerary(int PackageId, int? Id)
        {
            ViewData["PackageDetailId"] = PackageId;
            ViewData["IsGroupBooking"] = true;
            List<ItineraryAddUpdateModel> model = new();
            var data = await _unitOfWork.PackageItinerary.GetAll(x => x.PackageDetailId == PackageId).ToListAsync();
            model = data.Select(a => new ItineraryAddUpdateModel
            {
                Id = a.Id,
                Day = a.Day,
                Name = a.Name,
                Description = a.Description,
                PackageDetailId = a.PackageDetailId
            }).ToList();
            return View("~/Areas/Admin/Views/Package/EditItinerary.cshtml", model);
        }
        [HttpGet]
        public async Task<IActionResult> Flights(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = true ;
            var model = await _unitOfWork.PackageFlight.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View("~/Areas/Admin/Views/Package/Flights.cshtml", model);
        }
        [HttpGet]
        public async Task<IActionResult> AddFlight(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = true;


            var model = new List<PackageFlightAddUpdateModel>();
            var data = await _unitOfWork.PackageFlight.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            if (data is not null && data.Count > 0)
            {
                model = data.Select(a => new PackageFlightAddUpdateModel
                {
                    Id = a.Id,
                    Origin = a.Origin,
                    PackageDetailId = a.PackageDetailId,
                    Destination = a.Destination,
                    FlightNumber = a.FlightNumber,
                    PNRNumber = a.PNRNumber,
                    DepartureDate = a.DepartureDate,
                    ReturnDate = a.ReturnDate,
                    IsReturn = a.IsReturn,
                    IsMultiCity = a.IsMultiCity
                }).ToList();
            }

            return View("~/Areas/Admin/Views/Package/AddFlight.cshtml", model);
        }
    }
}
