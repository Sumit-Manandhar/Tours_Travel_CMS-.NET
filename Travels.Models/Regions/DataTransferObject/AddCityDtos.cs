using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Models.Regions.DataTransferObject
{
    public class AddCityDtos
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }

        public List<SelectListItem> Countries { get; set; }
    }
}
