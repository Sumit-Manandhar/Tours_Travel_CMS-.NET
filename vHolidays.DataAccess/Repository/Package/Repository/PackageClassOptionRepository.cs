using vHolidays.DataAccess.Repository.Package.Interface;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Package.DatabaseModel;

namespace vHolidays.DataAccess.Repository.Package.Repository
{
    public class PackageClassOptionRepository : Repository<PackageClassOption>, IPackageClassOptionRepository
    {
        public PackageClassOptionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
