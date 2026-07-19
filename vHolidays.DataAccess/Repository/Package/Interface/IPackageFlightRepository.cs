using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Interface
{
    public interface IPackageFlightRepository : IRepository<PackageFlight>
    {
        Task<ResponseModel<int>> CreateUpdate(List<PackageFlightAddUpdateModel> items);
        Task<ItineraryAddUpdateModel> Get(int Id);
    }
}
