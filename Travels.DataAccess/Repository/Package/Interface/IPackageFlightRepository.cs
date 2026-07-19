using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.DataAccess.Repository.IRepository;
using Travels.Models.Common;
using Travels.Models.Package.DatabaseModel;
using Travels.Models.Package.DataTransferObjects;

namespace Travels.DataAccess.Repository.Package.Interface
{
    public interface IPackageFlightRepository : IRepository<PackageFlight>
    {
        Task<ResponseModel<int>> CreateUpdate(List<PackageFlightAddUpdateModel> items);
        Task<ItineraryAddUpdateModel> Get(int Id);
    }
}
