using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Regions;
using vHolidays.Models.Regions.DataTransferObject;
using vHolidays.Utility;
using vHolidays.Utility.GetRegions;

namespace vHolidaysWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [AllowAnonymous]
    public class RegionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegionController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> AddRegions()
        {
            var utilities = await GetRegions.GetCountryData();
            await _unitOfWork.Country.AddRange(utilities);
            return RedirectToAction("Dashboard", "AdminDashboard", new { area = "Admin" });
        }

        public async Task AddState()
        {
            var utilities = await GetRegions.GetStateData();
            var country = await _unitOfWork.Country.GetActiveCountries("");
            foreach (var count in country)
            {
                var states = utilities.data.Where(x => x.iso3 == count.Alpha3Code).SelectMany(x => x.states)
                    .Select(x => new vHolidays.Models.Regions.State()
                    {
                        CountryId = count.Id,
                        Name = x.name,
                        StateCode = x.state_code,
                        IsActive = true,
                        IsPublished = true,
                    }).ToList();
                await _unitOfWork.Country.AddStateRange(states);
            }
        }
        public async Task<IActionResult> GetActiveCountries(string keyword)
        {
            var country = await _unitOfWork.Country.GetActiveCountries(keyword);
            return Json(country.Select(a => new
            {
                value = a.Id,
                text = a.CountryName
            }));
        }

        public async Task<IActionResult> GetCities(int countryId)
        {
            var city = await _unitOfWork.Country.GetActiveCity(countryId, "");
            return Json(city.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.CityName
            }));
        }

        public async Task<IActionResult> GetState(int countryId)
        {
            var state = await _unitOfWork.Country.GetActiveState(countryId, "");
            return Json(state.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }));
        }

        public async Task<IActionResult> Index(int countryId = 0, string keyword = "")
        {
            var city = await _unitOfWork.Country.GetCityPagination(keyword, PageNum, PageSize, countryId);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("CityList", city);

            city.Countries = await _unitOfWork.Country.GetActiveCountries("");
            return View(city);
        }


        public async Task<IActionResult> AddUpdate(int id = 0)
        {
            var model = new AddCityDtos { };
            if(id>0)
            {
                var city = await _unitOfWork.Country.GetCityById(id);
                if (city is not null)
                    model = new AddCityDtos
                    {
                        Id = city.Id,
                        CityName = city.CityName,
                        CountryId = city.CountryId,
                        IsDeleted = city.IsDeleted,
                        IsActive = city.IsActive,
                        IsPublished = city.IsPublished,
                    };
            }
            else
            {
                model.IsDeleted = false;
                model.IsActive = true;
                model.IsPublished = true;
            }
            var countries = await _unitOfWork.Country.GetActiveCountries("");
            model.Countries = countries.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.CountryName,
                Selected = model.CountryId == 0 ? false : model.CountryId == a.Id
            }).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCity(AddCityDtos model)
        {
            var responsse = await _unitOfWork.Country.CreateUpdateCiity(model);
            return Json(responsse);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUndeleteCity(int id)
        {
            var responsse = await _unitOfWork.Country.DeleteRestoreCity(id);
            return Json(responsse);
        }
    }
}
