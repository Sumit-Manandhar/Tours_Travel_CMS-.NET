using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using Travels.DataAccess.Repository.IRepository;
using Travels.DataAcess.Data;
using Travels.Models.Common;
using Travels.Models.Regions;
using Travels.Models.Regions.DataTransferObject;
using Travels.Utility.GetRegions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Travels.DataAccess.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _db;
        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task AddRange(CountriesApiModel obj)
        {
            if (_db.Country.Any())
            {
                return;
            }
            var countries = new List<Country>();
            var cities = new List<City>();
            foreach (var a in obj.data)
            {
                var country = new Country
                {
                    CountryName = a.name,
                    Currency = a.currency ?? "",
                    Flag = a.flag ?? "",
                    DialCode = a.dialCode ?? "",
                    Alpha2Code = a.iso2 ?? "",
                    Alpha3Code = a.iso3 ?? "",
                    IsDeleted = false,
                    IsActive = true,
                    IsPublished = true,
                    CreatedDate = DateTime.UtcNow,

                };
                countries.Add(country);
                if (a.cities is not null)
                {
                    foreach (var cityName in a.cities)
                    {
                        cities.Add(new City
                        {
                            CityName = cityName,
                            Country = country,
                            IsDeleted = false,
                            IsActive = true,
                            IsPublished = true,
                            CreatedDate = DateTime.UtcNow,
                            IsPrimary = true,
                        });
                    }
                }

            }

            await _db.Country.AddRangeAsync(countries);
            await _db.SaveChangesAsync();
            foreach (var city in cities)
            {
                city.CountryId = city.Country.Id;
            }
            await _db.City.AddRangeAsync(cities);
            await _db.SaveChangesAsync();

        }

        public async Task<List<City>> GetActiveCity(int CountryId, string keyword)
        {
            return await _db.City.Where(a => (CountryId == 0 || a.CountryId == CountryId) && a.IsPublished && a.IsActive && (string.IsNullOrEmpty(keyword) || a.CityName.ToLower().Contains(keyword.ToLower()))).OrderBy(a => a.CityName).ToListAsync();
        }
        public async Task<List<Models.Regions.State>> GetActiveState(int CountryId, string keyword)
        {
            return await _db.State.Where(a => a.CountryId == CountryId && a.IsPublished && a.IsActive && (string.IsNullOrEmpty(keyword) || a.Name.ToLower().Contains(keyword.ToLower()))).OrderBy(a => a.Name).ToListAsync();
        }
        public async Task<City> GetCityById(int Id)
        {
            return await _db.City.FirstOrDefaultAsync(a => a.Id == Id);
        }
        public async Task<List<Country>> GetActiveCountries(string keyword)
        {
            return await _db.Country.Where(a => a.IsPublished && a.IsActive && (string.IsNullOrEmpty(keyword) || a.CountryName.ToLower().Contains(keyword.ToLower()))).OrderBy(a => a.CountryName).ToListAsync();
        }
        public async Task<List<SelectListItem>> GetCountriesCurrency()
        {
            return await _db.Country.Where(a => a.IsPublished && a.IsActive).OrderBy(a => a.CountryName).Select(x => new SelectListItem()
            {
                Value = x.Currency,
                Text = x.Currency + $"({x.Alpha2Code})",
            }).ToListAsync();
        }

        public async Task<Country> GetCountryById(int Id)
        {
            return await _db.Country.FirstOrDefaultAsync(a => a.Id == Id);
        }


        public async Task<City> AddCityByDestionaiton(int destinationId, string cityName)
        {
            var country = _db.DestinationData.FirstOrDefault(q => q.DestinationId == destinationId)?.CountryId;
            var city = new City
            {
                CityName = cityName,
                CountryId = country.HasValue ? country.Value : 0,
                IsActive = true,
                IsDeleted = false,
                IsPublished = true,
                IsPrimary = false

            };
            _db.City.Add(city);
            await _db.SaveChangesAsync();
            return city;
        }

        public async Task AddStateRange(List<Models.Regions.State> stateList)
        {
            await _db.State.AddRangeAsync(stateList);
            await _db.SaveChangesAsync();
        }

        public async Task<CityPaginationModel> GetCityPagination(string keyword, int pageNumbere, int PageSize, int countryId = 0)
        {
            try
            {
                var count = await _db.City.Where(a =>
           (countryId == 0 || a.CountryId == countryId)
           && (string.IsNullOrEmpty(keyword) || a.CityName.ToLower().Contains(keyword.ToLower())
           )
           ).CountAsync();
                var cities = _db.City.Include(a => a.Country).Where(a =>
                (countryId == 0 || a.CountryId == countryId)
                && (string.IsNullOrEmpty(keyword) || a.CityName.ToLower().Contains(keyword.ToLower())
                )
                ).OrderBy(a => a.CityName).Skip((pageNumbere - 1) * PageSize)
                .Take(PageSize)
                .Select(a => new CityViewModel
                {
                    CityName = a.CityName,
                    Id = a.Id,
                    CountryId = a.CountryId,
                    CountryName = a.Country.CountryName,
                    isActive = a.IsActive,
                    IsDelete = a.IsDeleted,
                })
                .ToList();
                return new CityPaginationModel
                {
                    Cities = cities,
                    TotalRows = count,
                    PageNum = pageNumbere,
                    PageSize = PageSize
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ResponseModel<int>> CreateUpdateCiity(AddCityDtos model)
        {
            try
            {

                City city = new()
                {
                    Id = model.Id,
                    CountryId = model.CountryId,
                    CityName = model.CityName,
                    IsActive = model.IsActive,
                    IsPublished = model.IsPublished,
                    IsDeleted = model.IsDeleted,
                };
                if (model.Id == 0)
                {

                    await _db.AddAsync(city);
                }
                else
                {
                    var entity = _db.City.Find(model.Id);
                    if (entity is null)
                    {
                        return new ResponseModel<int>
                        {
                            Succeeded = false,
                            Message = "Invalid city",
                        };
                    }
                    entity.Id = model.Id;
                    entity.CountryId = model.CountryId;
                    entity.CityName = model.CityName;
                    entity.IsActive = model.IsActive;
                    entity.IsPublished = model.IsPublished;
                    entity.IsDeleted = model.IsDeleted;
                    _db.Update(entity);
                }
                await _db.SaveChangesAsync();
                return new ResponseModel<int>
                {
                    Data = city.Id,
                    Succeeded = true,
                    Message = "City Saved",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<int>
                {
                    Succeeded = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel<int>> DeleteRestoreCity(int id)
        {
            try
            {
                var entity = _db.City.Find(id);
                if (entity is null)
                {
                    return new ResponseModel<int>
                    {
                        Succeeded = false,
                        Message = "Invalid city",
                    };
                }

                entity.IsDeleted = !entity.IsDeleted;
                _db.Update(entity);
                await _db.SaveChangesAsync();
                return new ResponseModel<int>
                {

                    Succeeded = true,
                    Message = entity.IsDeleted ? "City Deleted" : "City Restored",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<int>
                {
                    Succeeded = false,
                    Message = ex.Message,
                };
            }

        }
    }
}
