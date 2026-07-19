using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vHolidays.Models.Regions.DataTransferObject
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public bool isActive { get; set; }
        public bool IsDelete { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
    public class CityPaginationModel
    {
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public List<CityViewModel> Cities { get; set; }
        public int TotalPage { get { return(int)Math.Ceiling(TotalRows / (double)PageSize); } }
        public List<Country> Countries { get; set; }
    }
}
