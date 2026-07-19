using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DataTransferObjects;
using Travels.Utility;
using Travels.Utility.Enumerations;

namespace TravelsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PackageController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public PackageController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index(string? keyword = "")
        {
            ViewData["IsGroupBooking"] = false;
            var Packages = _unitOfWork.Package.GetAll(a => !a.IsGroupBooking && a.Name.ToLower().Contains(keyword ?? "".ToLower())).ToList();
            return View(Packages);
        }

        [HttpGet]
        public async Task<IActionResult> Create(bool isGroup)
        {
            PackageDetailAddUpdateModel model = new();
            model.PackageType = isGroup ? GroupPackageTypeEnum.GroupBooking : GroupPackageTypeEnum.Package;
            model.Validity = DateOnly.FromDateTime(DateTime.Now);
            model.CountryId = 1;
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            model.IsPublished = true;
            model.IsActive = true;
            model.TourTypes = _unitOfWork.TourTypes.GetAll(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList();
            model.ClassOptionTypes = _unitOfWork.ClassOption.GetAll(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PackageDetailAddUpdateModel model)
        {
            if (model.PackageImg is not null)
                model.PackageImgUrl = await SaveFileAsync(model.PackageImg, "Package");
            var response = await _unitOfWork.Package.CreateUpdate(model);
            return Json(response);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            ViewData["IsGroupBooking"] = false;
            var model = await _unitOfWork.Package.Detail(Id);
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            model.BaseUrl = $"{Request.Scheme}://{Request.Host.Value}//";
            model.TourTypes = _unitOfWork.TourTypes.GetAll(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList();
            model.ClassOptionTypes = _unitOfWork.ClassOption.GetAll(a => a.IsActive && a.IsPublished && !a.IsDeleted).ToList();
            return View(model);
        }

        public async Task<IActionResult> Inclusion(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = false;
            var model = await _unitOfWork.PackageInclusion.GetAll(x => x.PackageDetailId == Id && !x.IsDeleted).ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddUpdateInclusion(int PackageId, int? Id, bool? isIncludes)
        {
            ViewData["IsGroupBooking"] = false;
            ViewData["PackageDetailId"] = PackageId;
            List<InclusionAddUpdateModel> model = new();
           var  data = await _unitOfWork.PackageInclusion.GetAll(x => x.PackageDetailId == PackageId && !x.IsDeleted).ToListAsync();
            if(data is not null && data.Count >0)
            {
                model = data.Select(a => new InclusionAddUpdateModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    PackageDetailId = PackageId,
                    IsIncluded = a.IsIncluded
                }).ToList();
            }

            return PartialView(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddUpdateInclusion(InclusionAddUpdateModel model)
        //{
        //    var response = await _unitOfWork.PackageInclusion.CreateUpdate(model);
        //    return Json(response);
        //}

        [HttpPost]
        public async Task<IActionResult> AddUpdateInclusion(SaveIncusionModel model)
        {
            foreach(var i in model.Inclusions)
            {
                i.PackageDetailId = model.PackageDetailId;
            }
            var response = await _unitOfWork.PackageInclusion.CreateUpdate(model.Inclusions);
            return Json(response);
        }
        public async Task<IActionResult> Itinerary(int Id)
        {
            ViewData["IsGroupBooking"] = false;
            ViewData["PackageDetailId"] = Id;
            var model = await _unitOfWork.PackageItinerary.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddUpdateItinerary(int PackageId, int? Id)
        {
            ViewData["PackageDetailId"] = PackageId;
            ViewData["IsGroupBooking"] = false;

            List<ItineraryAddUpdateModel> model = new();           
            var data = await _unitOfWork.PackageItinerary.GetAll(x => x.PackageDetailId == PackageId && !x.IsDeleted).ToListAsync();
            model = data.Select(a => new ItineraryAddUpdateModel
            {
                Id = a.Id,
                Day = a.Day,
                Name = a.Name,
                Description = a.Description,
                PackageDetailId = a.PackageDetailId
            }).ToList();
            return View("EditItinerary",model);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddUpdateItinerary(ItineraryAddUpdateModel model)
        //{
        //    var response = await _unitOfWork.PackageItinerary.CreateUpdate(model);
        //    return Json(response);
        //} 
        [HttpPost]
        public async Task<IActionResult> AddUpdateItinerary(SaveItineraryModel model)
        {
            foreach (var i in model.Itineraries)
            {
                i.PackageDetailId = model.PackageDetailId;

            }
            var response = await _unitOfWork.PackageItinerary.CreateUpdate(model.Itineraries);

            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> Images(int Id)
        {
            ViewData["IsGroupBooking"] = false;
            ViewData["PackageDetailId"] = Id;
            var model = await _unitOfWork.PackageImage.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateImages(PackageImageCreateModel model)
        {
            var images = Request?.Form?.Files?.Where(a => a.Name.StartsWith("Images[")).ToList();
            List<string> ImgUrls = new();
            if (images != null && images.Any())
            {
                foreach (var img in images)
                {
                    ImgUrls.Add(await SaveFileAsync(img, $"Package/{model.Id}"));
                }
            }
            var response = await _unitOfWork.PackageImage.SaveImages(model.Id, ImgUrls, model.toDeleteImages);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> Hotels(int Id)
        {
            ViewData["IsGroupBooking"] = false;
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
            return View(model);
        }

        public async Task<IActionResult> EditHotel(PackageHotelViewModel model)
        {
            var response = await _unitOfWork.PackageHotel.CreateUpdate(model);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHotel(int id, int packageId)
        {
            var response = await _unitOfWork.PackageHotel.DeleteHotelPackages(id, packageId);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> PackageGroup(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = false;
            var model = await _unitOfWork.PackageGroup.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddUpdateGroup(int PackageId, int? Id)
        {
            GroupAddUpdateModel model = new();
            if (Id.HasValue && Id.Value > 0)
            {
                model = await _unitOfWork.PackageGroup.Get(Id.Value);
            }
            else
            {
                model.PackageDetailId = PackageId;
                model.IsActive = true;
                model.IsPublished = true;
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateGroup(GroupAddUpdateModel model)
        {
            var response = await _unitOfWork.PackageGroup.CreateUpdate(model);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveGroupOrder(List<DataOrderingModel> model)
        {
            var response = await _unitOfWork.PackageGroup.UpdateOrder(model);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> Price(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = false;
            var model = await _unitOfWork.PackagePrice.GetAll(Id, true);
            return View("PackagePrice", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdatePrice(PriceViewModel model)
        {
            var response = await _unitOfWork.PackagePrice.Update(model.PriceList);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> Sync(int id)
        {
            return View("_partialSyncpackage", new SyncPackageDTO());
        }

        [HttpPost]
        public async Task<IActionResult> SyncToGroupDeparture(SyncPackageDTO model)
        {
            var response = await _unitOfWork.Package.SyncToGroupBooking(model);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> Flights(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = false;
            var model = await _unitOfWork.PackageFlight.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddFlight(int Id)
        {
            ViewData["PackageDetailId"] = Id;
            ViewData["IsGroupBooking"] = false;


            var model = new List<PackageFlightAddUpdateModel>();
            var data = await _unitOfWork.PackageFlight.GetAll(x => x.PackageDetailId == Id).ToListAsync();
            if(data is not null && data.Count>0)
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

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateFlight(SaveFlightModel model)
        {
            foreach(var i in model.Flights)
            {
                i.PackageDetailId = model.PackageDetailId;
            }
            var response = await _unitOfWork.PackageFlight.CreateUpdate(model.Flights);

            return Json(new ResponseModel<int> { Succeeded = true, Message = "Saved" });
        }
    }
}
