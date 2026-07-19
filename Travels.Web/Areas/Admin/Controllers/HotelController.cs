using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travels.DataAccess.Repository;
using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Hotel;
using Travels.Models.Package.DataTransferObjects;
using Travels.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class HotelController : BaseController
    {
        private readonly IUnitOfWork _uow;
        public HotelController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _uow = unitOfWork;
        }

        public IActionResult Index(string? keyword="")
        {
            var hotel = _uow.Hotel.GetHotels(keyword);
            return View(hotel);
        }

        public async Task<IActionResult> Create(int Id = 0, int? countryId = 0, int? packageId = 0, string? redirectUrl = "")
        {
            var model = new HotelListViewModel { Id = Id };
            model.Countries = await _uow.Country.GetActiveCountries("");
            if (Id > 0)
            {
                var data = _uow.Hotel.Get(a => a.Id == Id);

                model.HotelName = data.HotelName;
                model.AddressLine = data.AddressLine;
                model.CountryId = data.CountryId;
                model.CityId = data.CityId;
                model.DisplayImageUrl = data.DisplayImageUrl;
                model.IsActive = data.IsActive;
                model.IsDeleted = data.IsDeleted;
                model.IsPublished = data.IsPublished;
            }
            else
            {
                model.IsActive = true;
                model.IsDeleted = false;
                model.IsPublished = true;
                model.CountryId = (countryId.HasValue && countryId > 0) ? countryId.Value : model.Countries.FirstOrDefault()?.Id ?? 0;
            }
            model.PackageId = packageId;
            model.Cities = await _uow.Country.GetActiveCity(model.CountryId, "");
            model.RedirectUrl = redirectUrl ?? "";
            var scheme = Request.Scheme;
            var host = Request.Host.Value;
            model.BaseUrl = $"{scheme}://{host}//";
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(HotelListViewModel model)
        {
            if (model.Image is not null)
                model.DisplayImageUrl = await SaveFileAsync(model.Image, "Hotel");
            var response = await _uow.Hotel.CreateUpdate(model);
            if (response.Succeeded && response.Data > 0 && model.PackageId.HasValue && model.PackageId.Value > 0)
            {
                var hotelresp = await _uow.PackageHotel.CreateUpdate(new PackageHotelViewModel
                {
                    PackageId = model.PackageId.Value,
                    HotelId = new int[] { response.Data }
                });
                return Json(hotelresp);


            }
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> SwitchDelete(int id)
        {
            var response = await _uow.Hotel.OnOffDelete(id);
            return Json(response);

        }
    }
}
