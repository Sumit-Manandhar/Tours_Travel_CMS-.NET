using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Models.Common;
using Travels.Models.Hotel;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface IHotelRepository :IRepository<Hotel>
    {
        public List<Hotel> GetHotels(string?keword="");
        Task<ResponseModel<int>> CreateUpdate(HotelListViewModel model);
        Task<ResponseModel<int>> OnOffDelete(int id);
        List<Hotel> GetActiveten(int countryId, int? CityId = 0);
    }
}
