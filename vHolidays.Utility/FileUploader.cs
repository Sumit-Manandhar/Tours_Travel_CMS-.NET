using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vHolidays.Utility
{
    public class FileUploader
    {
        public static async Task<string> SaveFileAsync(IFormFile file, IWebHostEnvironment _env, string folderName)
        {
            try
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, folderName);
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
                return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
            }
            catch (Exception)
            {
                // Handle exceptions or return null if the save fails
                return null;
            }
        }
    }
}
