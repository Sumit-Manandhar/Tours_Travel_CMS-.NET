using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.SubDestination.DataTransferObjects;
using vHolidays.Models.ViewModels;
using vHolidays.Utility;

namespace vHolidaysWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SubDestinationController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubDestinationController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int DestinationId, string? keyword = "")
        {
            var destination = _unitOfWork.Subdestination.GetAll(c => c.DestinationId == DestinationId && c.Name.ToLower().Contains(keyword ?? "".ToLower())).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
            ViewData["DestinationId"] = DestinationId;
            return View(destination);
        }

        public async Task<IActionResult> Create(int DestinationId, int? Id = 0)
        {
            SubDestinationAddUpdateModel model = new();
            model.DestinationId = DestinationId;
            var destination = _unitOfWork.Destination.GetById(DestinationId);
            model.CountryId = destination.CountryId;
            model.DestinationName = destination.Name;
            if (Id.HasValue && Id.Value > 0)
            {
                model.Id = Id.Value;
                var data = _unitOfWork.Subdestination.GetById(Id.Value);
                if (data is not null)
                {

                    model.CityId = data.CityId;
                    model.ShortDesc = data.ShortDescription;
                    model.Description = data.Description;
                    model.TopDestinations = data.TopDestination;
                    model.IsDeleted = data.IsDeleted;
                    model.IsActive = data.IsActive;
                    model.IsPublished = data.IsPublished;
                    model.BannerImageUrl = data.Images.FirstOrDefault(a => a.IsBanner)?.ImageUrl ?? "";
                    model.DisplayImageUrl = data.Images.FirstOrDefault(a => a.IsPrimary)?.ImageUrl ?? "";
                    model.AdditionalImages = data.Images.Where(a => !a.IsBanner && !a.IsPrimary).ToDictionary(a => a.Id, a => a.ImageUrl); ;
                }

            }
            var scheme = Request.Scheme;
            var host = Request.Host.Value;
            model.BaseUrl = $"{scheme}://{host}//"; // replace if we change file save instance
            model.Cities = await _unitOfWork.Country.GetActiveCity(destination.CountryId, "");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateSubDestination(SubDestinationAddUpdateModel model)
        {
            try

            {
                var test = Request.Form;
                var images = Request?.Form?.Files?.Where(a => a.Name.StartsWith("Images[")).ToList();

                if (model.DisplayImg is not null)
                    model.DisplayImageUrl = await SaveFileAsync(model.DisplayImg, "DestinationImg");
                if (model.BannerImg is not null)
                    model.BannerImageUrl = await SaveFileAsync(model.BannerImg, "DestinationImg");

                if (images is not null && images.Count > 0)
                {
                    model.ImageUrls = new List<string>();
                    foreach (var img in images)
                    {
                        model.ImageUrls.Add(await SaveFileAsync(img, "DestinationImg"));
                    }
                }
                var request = new SaveSubdestinationModel
                {
                    Id = model.Id,
                    DestinationId = model.DestinationId,

                    ShortDesc = model.ShortDesc,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    IsPublished = model.IsPublished,
                    IsDeleted = model.IsDeleted,
                    TopDestination = model.TopDestinations,
                    DisplayImageUrl = model.DisplayImageUrl,
                    BannerImageUrl = model.BannerImageUrl,
                    ImageUrls = model.ImageUrls,
                    toDeleteImages = model.toDeleteImages,
                };
                if (model.CityId == 0)
                {
                    if (string.IsNullOrEmpty(model.SubDestinationName))
                    {
                        return BadRequest("invalid city");
                    }
                    var city = await _unitOfWork.Country.AddCityByDestionaiton(model.DestinationId, model.SubDestinationName);
                    request.CityId = city.Id;
                    request.SubdestinationName = city.CityName;
                }
                else
                {
                    request.CityId = model.CityId;
                    request.SubdestinationName = _unitOfWork.Country.GetCityById(model.CityId)?.Result?.CityName ?? "";
                }

                if (model.toDeleteImages is not null)
                {

                }
                await _unitOfWork.Subdestination.Add(request);
                return Ok(model);

            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Message = ex.Message,
                    ErrorCode = 500
                };

                return BadRequest(errorResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SwitchDelete(int id)
        {
            try
            {
                await _unitOfWork.Subdestination.OnOffDelete(id);
                return Ok(new
                {
                    Message = "Sub Destination Saved!",

                });
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

        //public async Task<IActionResult> AddUpdate(int id, int DestinationId)
        //{
        //    var model = new SubdestinationViewModel();

        //    var destination = _unitOfWork.Destination.GetById(DestinationId);
        //    model.DestinationName = destination?.Name ?? "";
        //    model.DestinationId = DestinationId;
        //    model.Cities = await _unitOfWork.Country.GetActiveCity(destination.CountryId, "");
        //    if (id > 0)
        //        model.Data = _unitOfWork.Subdestination.GetById(id);
        //    return PartialView("_addUpdate", model);
        //}

        public async Task<IActionResult> Details(int id)
        {
            var data = _unitOfWork.Subdestination.GetById(id);
            return View(data);
        }

        //[HttpPost]
        //public async Task<IActionResult> SaveSubDestination([FromForm] SaveSubdestinationModel model)
        //{
        //    try
        //    {
        //        var test = Request.Form;
        //        var images = Request?.Form?.Files?.Where(a => a.Name.StartsWith("Images[")).ToList();

        //        if (model.DisplayImg is not null)
        //            model.DisplayImageUrl = await SaveFileAsync(model.DisplayImg);
        //        if (model.BannerImg is not null)
        //            model.BannerImageUrl = await SaveFileAsync(model.BannerImg);

        //        if (images is not null && images.Count > 0)
        //        {
        //            model.ImageUrls = new List<string>();
        //            foreach (var img in images)
        //            {
        //                model.ImageUrls.Add(await SaveFileAsync(img));
        //            }
        //        }
        //        await _unitOfWork.Subdestination.Add(model);
        //        var successResponse = new
        //        {
        //            Message = "Destination Saved!",

        //        };

        //        return Ok(successResponse);

        //    }
        //    catch (Exception ex)
        //    {
        //        var errorResponse = new
        //        {
        //            Message = ex.Message,
        //            ErrorCode = 500
        //        };

        //        return BadRequest(errorResponse);
        //    }


        //}


    }
}
