using Microsoft.EntityFrameworkCore;
using vHolidays.DataAccess.Repository.SupplyPartner.Interface;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Common;
using vHolidays.Models.SupplyPartner.DatabaseModel;
using vHolidays.Models.SupplyPartner.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.SupplyPartner.Repository
{
    public class SupplierDetailRepository : Repository<SupplierDetail>, ISupplierDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public SupplierDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<string>> Create(SupplierDetailCreateModel model)
        {
            try
            {
                SupplierDetail data = new()
                {
                    Name = model.Name,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    CountryId = model.CountryId,
                    StateId = model.StateId,
                    CityId = model.CityId,
                    Zip = model.Zip,
                    Currency = model.Currency,
                    UpdatedTel = model.UpdatedTel,
                    UpdatedFax = model.UpdatedFax,
                    Website = model.Website,
                    CentralEmail = model.CentralEmail,
                    ReservationEmail1 = model.ReservationEmail1,
                    ReservationEmail2 = model.ReservationEmail2,
                    IsActive = true,
                    IsPublished = true,
                    License = model.License,
                    Destination = model.Destination.Select(x => new SupplierDestination()
                    {
                        CountryId = x.CountryId,
                        City = x.CityId.Select(ct => new SupplierDestinationCity()
                        {
                            CityId = ct,
                        }).ToList()
                    }).ToList(),
                    Product = model.Product.Select(x => new SupplierProducts()
                    {
                        ProductId = x.ProductId,
                        SupplierName = x.SupplierName,
                    }).ToList(),
                    Contact = model.Contact.Select(x => new SupplierContact()
                    {
                        Type = x.Type,
                        Name = x.Name,
                        Designation = x.Designation,
                        Email = x.Email,
                        DirectNo = x.DirectNo,
                        Mobile = x.Mobile,
                        IsActive = true,
                        IsPublished = true,
                    }).ToList()
                };
                Add(data);
                await _db.SaveChangesAsync();
                return new ResponseModel<string>()
                {
                    Succeeded = true,
                    Message = "Vendor registration success."
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>()
                {
                    Succeeded = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<SupplierDetailDto> Detail(int id)
        {
            var data = await _db.SupplierDetail.Where(x=> x.Id == id).Include(x => x.Country).Include(x => x.State).Include(x => x.City)
                .Include(x => x.Destination).ThenInclude(d => d.City).ThenInclude(ct => ct.City)
                .Include(x => x.Destination).ThenInclude(d => d.Country)
                .Include(x => x.Product).Include(x => x.Contact).Select(x => new SupplierDetailDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    CountryId = x.CountryId,
                    Country = x.Country.CountryName,
                    StateId = x.StateId,
                    State = x.State.Name,
                    CityId = x.CityId,
                    City = x.City.CityName,
                    Zip = x.Zip,
                    Currency = x.Currency,
                    UpdatedTel = x.UpdatedTel,
                    UpdatedFax = x.UpdatedFax,
                    Website = x.Website,
                    CentralEmail = x.CentralEmail,
                    ReservationEmail1 = x.ReservationEmail1,
                    ReservationEmail2 = x.ReservationEmail2,
                    License = x.License,
                    Destination = x.Destination.Select(d => new SupplierDestinationDto()
                    {
                        Id = d.Id,
                        CountryId = x.CountryId,
                        Country = d.Country.CountryName,
                        City = d.City.Select(ct => new SupplierDestinationCityDto()
                        {
                            Id = ct.Id,
                            CityId = ct.CityId,
                            City = ct.City.CityName
                        }).ToList()
                    }).ToList(),
                    Product = x.Product.Select(p => new SupplierProductsDto()
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        SupplierName = p.SupplierName,
                    }).ToList(),
                    Contact = x.Contact.Select(x => new SupplierContactDto()
                    {
                        Id = x.Id,
                        Type = x.Type,
                        Name = x.Name,
                        Designation = x.Designation,
                        Email = x.Email,
                        DirectNo = x.DirectNo,
                        Mobile = x.Mobile
                    }).ToList()
                }).FirstOrDefaultAsync();
            return data;
        }

    }
}
