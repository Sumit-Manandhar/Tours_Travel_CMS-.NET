using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Models.Settings.DataTransferObjects
{
    public class SaveContactsdto
    {
        public string ContactNumber { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactAddress { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? MapLinks { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? YoutubeLink { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public string UserName { get; set; }
    }
}
