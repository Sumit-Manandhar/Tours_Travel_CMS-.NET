using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vHolidays.Models.Regions;

namespace vHolidays.Models.Settings.DataTransferObjects
{
    public class BranchesDTO
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchType { get; set; } = null!;
        public string BranchCountry { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string OptionalNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }
        public string UserName { get; set; }
        public string? BaseUrl { get; set; } = string.Empty;
        public List<Country> Countries { get; set; }
    }
}
