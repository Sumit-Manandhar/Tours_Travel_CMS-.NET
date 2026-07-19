using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travels.DataAccess.Repository.IRepository;
using Travels.Utility;

namespace TravelsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ContactUsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactUsController(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork, env) 
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var contacts = _unitOfWork.Contacts
                .GetAll()
                .Skip((PageNum-1) * PageSize)
                .Take(PageSize)
                .ToList();
            return View(contacts);
        }
    }
}
