using Travels.DataAccess.Repository.Package.Interface;
using Travels.DataAcess.Data;
using Travels.Models.Package.DatabaseModel;

namespace Travels.DataAccess.Repository.Package.Repository
{
    public class PackageClassOptionRepository : Repository<PackageClassOption>, IPackageClassOptionRepository
    {
        public PackageClassOptionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
