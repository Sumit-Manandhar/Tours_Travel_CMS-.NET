using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Settings.DataTransferObjects;
using vHolidays.Utility;

namespace vHolidaysWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ContactController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public ContactController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public IActionResult Index()
        {
            var contact = _unitOfWork.Contacts.GetAll().FirstOrDefault(a => a.IsActive && a.IsPublished && !a.IsDeleted);

            return View(contact?? new vHolidays.Models.Settings.Contacts());
        }
        [HttpPost]
        public async Task<IActionResult> SaveContact(SaveContactsdto model)
        {
            try
            {
                model.UserName = User?.Identity?.Name ?? "";
                await _unitOfWork.Contacts.Update(model);
                var successResponse = new
                {
                    Message = "Contact Saved!",
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

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var data = _unitOfWork.ContactUsMessages.GetAll().ToList();
            return View(data);
        }
    }
}
