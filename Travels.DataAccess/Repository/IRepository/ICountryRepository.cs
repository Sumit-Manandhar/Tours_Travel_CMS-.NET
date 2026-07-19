using Microsoft.AspNetCore.Mvc.Rendering;
using Travels.Models.Common;
using Travels.Models.Regions;
using Travels.Models.Regions.DataTransferObject;
using Travels.Utility.GetRegions;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface ICountryRepository
    {
        Task AddRange(CountriesApiModel obj);
        Task<List<Country>> GetActiveCountries(string keyword);
        Task<List<SelectListItem>> GetCountriesCurrency();
        Task<List<City>> GetActiveCity(int CountryId, string keyword);
        Task<List<Models.Regions.State>> GetActiveState(int CountryId, string keyword);
        Task<Country> GetCountryById(int Id);
        Task<City> GetCityById(int Id);
        Task<City> AddCityByDestionaiton(int destinationId, string cityName);
        Task AddStateRange(List<Models.Regions.State> stateList);
        Task<CityPaginationModel> GetCityPagination(string keyword, int pageNumbere, int PageSize, int countryId = 0);
         Task<ResponseModel<int>> CreateUpdateCiity(AddCityDtos model);
        Task<ResponseModel<int>> DeleteRestoreCity(int id);
    }
}
