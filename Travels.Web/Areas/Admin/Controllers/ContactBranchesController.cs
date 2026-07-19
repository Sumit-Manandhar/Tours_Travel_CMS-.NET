using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Settings.DataTransferObjects;
using Travels.Utility;

namespace TravelsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ContactBranchesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public ContactBranchesController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IActionResult> Index(string? keyword = "")
        {
            var data = await _unitOfWork.ContactBranches.GetAll();
            var response = data.Where(a => a.BranchName.ToLower().Contains(keyword ?? "".ToLower())).ToList();
            return View(response);
        }
        public async Task<IActionResult> Create(int id)
        {
            var model = new BranchesDTO();
            model.Id = id;
            if (id > 0)
            {
                var data = _unitOfWork.ContactBranches.Get(a => a.Id == id);
                if (data is not null)
                {
                    model.BranchName = data.BranchName;
                    model.BranchCountry = data.BranchCountry;
                    model.BranchType = data.BranchType;
                    model.IsActive = data.IsActive;
                    model.IsPublished = data.IsPublished;
                    model.Address = data.Address;
                    model.ContactNumber = data.ContactNumber;
                    model.Email = data.Email;
                    model.OptionalNumber = data.OptionalNumber;
                    model.ImageURL = data.CompanyImage;
                }
            }
            var scheme = Request.Scheme;
            var host = Request.Host.Value;
            model.BaseUrl = $"{scheme}://{host}//"; // replace if we change file save instance
            model.Countries = await _unitOfWork.Country.GetActiveCountries("");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveBranches(BranchesDTO model)
        {

            model.UserName = User?.Identity?.Name ?? "";
            if (model.Image is not null)
                model.ImageURL = await FileUploader.SaveFileAsync(model.Image, _env, "ContactBranches");
            try
            {
                var id = _unitOfWork.ContactBranches.AddUpdate(model);
                return Json(new ResponseModel<int> { Data = id.Result, Message = "branch Saved", Succeeded = true });
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

        [HttpPost]
        public async Task<IActionResult> SwitchDelete(int id)
        {
            try
            {
                await _unitOfWork.ContactBranches.OnOffDelete(id);
                return Json(new ResponseModel<int> { Data = id, Message = "branch Saved", Succeeded = true });

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

        #region agents
        [HttpGet]
        public async Task<IActionResult> Agents(int id)
        {
            var data = await _unitOfWork.BranchAgents.GetAll(id);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAgents(int ContactBranchId, int id)
        {
            var model = new AgentsDTO();
            model.ContactBranchId = ContactBranchId;
            if (id > 0)
            {
                model = await _unitOfWork.BranchAgents.Get(id);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAgents(AgentsDTO model)
        {
            try
            {
                var id = await _unitOfWork.BranchAgents.AddUpdate(model);


                return Json(new ResponseModel<int> { Data = id, Message = "Agent Saved", Succeeded = true });
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

        [HttpPost]
        public async Task<IActionResult> SwitchDeleteAgent(int id)
        {
            try
            {
                await _unitOfWork.BranchAgents.OnOffDelete(id);
                return Json(new ResponseModel<int> { Data = id, Message = "Agent Saved", Succeeded = true });

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
        #endregion
    }
}
