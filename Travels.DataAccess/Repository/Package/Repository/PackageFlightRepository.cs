using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.DataAccess.Repository.Package.Interface;
using Travels.DataAcess.Data;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Repository
{
    public class PackageFlightRepository : Repository<PackageFlight>, IPackageFlightRepository
    {

        private readonly ApplicationDbContext _db;

        public PackageFlightRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<int>> CreateUpdate(List<PackageFlightAddUpdateModel> items)
        {
            try
            {
                foreach(var i in items)
                {
                    PackageFlight data = new()
                    {
                        Id = i.Id,
                        PackageDetailId = i.PackageDetailId,
                        Origin = i.Origin,
                        Destination = i.Destination,
                        FlightNumber = i.FlightNumber,
                        PNRNumber = i.PNRNumber,
                        DepartureDate = i.DepartureDate,
                        ReturnDate = i.ReturnDate,
                        IsReturn = i.IsReturn,
                        IsMultiCity = i.IsMultiCity,

                    };
                    Update(data);
                    await _db.SaveChangesAsync();
                    i.Id = data.Id;
                }

                var allEntity = _db.PackageFlight.Where(a => a.PackageDetailId == items.FirstOrDefault().PackageDetailId).ToList();

                var toDelete = allEntity.Where(a => !items.Select(c => c.Id).Contains(a.Id)).ToList();
                if (toDelete.Count > 0)
                    _db.PackageFlight.RemoveRange(toDelete);
                await _db.SaveChangesAsync();

                return new ResponseModel<int>
                {
                    Data = 0,
                    Succeeded = true,
                    Message = "Itinerary Saved",
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

        public Task<ItineraryAddUpdateModel> Get(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
