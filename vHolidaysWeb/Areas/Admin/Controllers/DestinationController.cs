using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Destinations.DataTransferObjects;
using vHolidays.Models.ViewModels;
using vHolidays.Utility;

namespace vHolidaysWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DestinationController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public DestinationController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index(string? keyword)
        {
            var destination = _unitOfWork.Destination.
                GetAll().Where(a=>a.Name.Contains( keyword??""))
                .Skip((PageNum - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            return View(destination);
        }

        public async Task<IActionResult> Create(int? Id =0)
        {
            DestinationAddUpdateModel model = new();
            model.CountryId = 1;
            if(Id.HasValue && Id.Value>0)
            {
                model.Id = Id.Value;
                var data = _unitOfWork.Destination.GetById(Id.Value);
                if(data is not null)
                {
                    model.CountryId = data.CountryId;
                    model.Name = data.Name;
                    model.CountryName = data.CountryName;
                    model.ShortDescription = data.ShortDescription;
                    model.Description = data.Description;
                    model.Overview = data.Overview;
                    model.IsDeleted = data.IsDeleted;
                    model.IsActive = data.IsActive;
                    model.IsPublished = data.IsPublished;
                    model.BannerImgUrl = data.Images.FirstOrDefault(a => a.IsBanner)?.ImageUrl ?? "";                  
                }

            }
            var scheme = Request.Scheme;
            var host = Request.Host.Value;
            model.BaseUrl = $"{scheme}://{host}//"; // replace if we change file save instance
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateDestination(DestinationAddUpdateModel model)
        {
            try
            {
                var request = new SaveDestinationModel();
                if (model.BannerImg is not null)
                {
                    request.BannerImageUrl = await SaveFileAsync(model.BannerImg, "DestinationImg");

                    //model.BannerImgUrl remove image from foler                 
                }
                request.Id = model.Id;
                request.CountryId = model.CountryId;
                request.DestinationName = _unitOfWork.Country.GetCountryById(model.CountryId)?.Result?.CountryName??"";
                request.ShortDesc = model.ShortDescription;
                request.Description = model.Description;
                request.Overview = model.Overview;
                request.IsActive = model.IsActive;
                request.IsPublished = model.IsPublished;
                model.Id=await _unitOfWork.Destination.Add(request);
                var successResponse = new
                {
                    Message = "Destination Saved!",
                    Id = model.Id,

                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    ErrorCode = 500
                });
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            var data = _unitOfWork.Destination.GetById(id);
            return View(data);
        }

        [HttpPost]
        public async Task <IActionResult> SwitchDelete(int id)
        {
            try
            {
                await _unitOfWork.Destination.OnOffDelete(id);
                return Ok( new
                {
                    Message = "Destination Saved!",

                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    ErrorCode = 500
                });
            }
        }
    }
}
