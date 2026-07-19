using Microsoft.AspNetCore.Http;

namespace Travels.Models.Package.DataTransferObjects
{
    public class PackageImageCreateModel
    {
        public int Id { get; set; }
        public List<IFormFile> Images { get; set; }
        public int[]? toDeleteImages { get; set; }
    }
}
