using Microsoft.AspNetCore.Mvc;
using vHolidays.DataAccess.Repository.IRepository;

namespace vHolidaysWeb.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        int _pageNum = Int32.MinValue, _pageSize = Int32.MinValue;

        #region Pagination setting
        public int PageNum
        {

            get
            {
                try
                {
                    if ((!Int32.TryParse(Request.Query["PageNum"].FirstOrDefault(), out _pageNum) &&
                         !Int32.TryParse(Request.Form["PageNum"].FirstOrDefault(), out _pageNum)) ||
                         _pageNum <= 0)
                    {
                        _pageNum = 1;
                    }
                }
                catch (Exception ex)
                {

                    if ((!Int32.TryParse(Request.Query["PageNum"].FirstOrDefault(), out _pageNum)) ||
                                       _pageNum <= 0)
                    {
                        _pageNum = 1;
                    }
                }


                return _pageNum;
            }
        }

        public int PageSize
        {
            get
            {
                try
                {
                    if ((!Int32.TryParse(Request.Query["PageSize"].FirstOrDefault(), out _pageSize) &&
                !Int32.TryParse(Request.Form["PageSize"].FirstOrDefault(), out _pageSize)) ||
                _pageSize <= 0)
                    {
                        _pageSize = 10;//DefaultPageSizes.Default;
                    }
                }
                catch (Exception)
                {

                    if ((!Int32.TryParse(Request.Query["PageSize"].FirstOrDefault(), out _pageSize)) ||
                _pageSize <= 0)
                    {
                        _pageSize = 10;//DefaultPageSizes.Default;
                    }
                }

                return _pageSize;
            }
        }

        public string OrderBy
        {
            get
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Request.Query["OrderBy"].FirstOrDefault()))
                        return Request.Query["OrderBy"].FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(Request.Form["OrderBy"].FirstOrDefault()))
                        return Request.Form["OrderBy"].FirstOrDefault();
                }
                catch (Exception)
                {
                    if (!string.IsNullOrWhiteSpace(Request.Query["OrderBy"].FirstOrDefault()))
                        return Request.Query["OrderBy"].FirstOrDefault();
                }

                return null;
            }
        }

        public bool OrderByAscending
        {
            get
            {
                bool temp = false;
                try
                {
                    if (!bool.TryParse(Request.Query["OrderByAscending"].FirstOrDefault(), out temp))
                        if (!bool.TryParse(Request.Form["OrderByAscending"].FirstOrDefault(), out temp))
                            return false;
                }
                catch (Exception)
                {

                    if (!bool.TryParse(Request.Query["OrderByAscending"].FirstOrDefault(), out temp))
                        return false;
                }

                return temp;
            }
        }
        #endregion

        protected async Task<string> SaveFileAsync(IFormFile file, string location)
        {
            try
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, location);
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                var extension = Path.GetExtension(file.FileName);
                if (string.IsNullOrEmpty(extension))
                {
                    if (file.ContentType == "image/jpeg")
                    {
                        extension = ".jpg";
                    }
                    else if (file.ContentType == "image/png")
                    {
                        extension = ".png";
                    }
                    else if (file.ContentType == "image/gif")
                    {
                        extension = ".gif";
                    }
                    else if (file.ContentType == "image/bmp")
                    {
                        extension = ".bmp";
                    }
                    else if (file.ContentType == "image/tiff")
                    {
                        extension = ".tiff";
                    }
                    else if (file.ContentType == "image/webp")
                    {
                        extension = ".webp";
                    }
                    else if (file.ContentType == "image/svg+xml")
                    {
                        extension = ".svg";
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported file type.");
                    }
                }
                var uniqueFileName = Guid.NewGuid().ToString() + extension;
                // Set the full path for the file
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // Return the relative URL (for example: /images/filename.ext)
                return Path.Combine(location, uniqueFileName).Replace("\\", "/");
            }
            catch (Exception)
            {
                // Handle exceptions or return null if the save fails
                return null;
            }
        }
    }
}
