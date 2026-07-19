using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Master.DatabaseModel;
using Travels.Models.Master.DataTransferObjects;
using Travels.Models.Package.DataTransferObjects;
using Travels.Utility;

namespace TravelsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MasterDataController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public MasterDataController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var data = _unitOfWork.TourTypes.GetAll().ToList();
            return View(data);
        }

        public IActionResult AddTour(int id)
        {
            var model = new TourTypeDTO();
            if (id > 0)
            {
                var data = _unitOfWork.TourTypes.Get(a => a.Id == id);
                model.Id = data.Id;
                model.IsActive = data.IsActive;
                model.Name = data.Name;
                model.Description = data.Description;
                model.IsPublished = data.IsPublished;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateTour(TourTypeDTO model)
        {
      
            var response = await _unitOfWork.TourTypes.CreateUpdate(model);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SwitchDeleteTours(int id)
        {
            var response = await _unitOfWork.TourTypes.OnOffDelete(id);
            return Json(response);          
        }

        public IActionResult ClassOption()
        {
            var data = _unitOfWork.ClassOption.GetAll().ToList();
            return View(data);
        }

        public IActionResult AddClassOption(int id)
        {
            var model = new ClassOptionDto();
            if (id > 0)
            {
                var data = _unitOfWork.ClassOption.Get(a => a.Id == id);
                model.Id = data.Id;
                model.IsActive = data.IsActive;
                model.Name = data.Name;
                model.Description = data.Description;
                model.IsPublished = data.IsPublished;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateClassOption(ClassOptionDto model)
        {

            var response = await _unitOfWork.ClassOption.CreateUpdate(model);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SwitchDeleteClassOption(int id)
        {
            var response = await _unitOfWork.ClassOption.OnOffDelete(id);
            return Json(response);
        }
    }
}
