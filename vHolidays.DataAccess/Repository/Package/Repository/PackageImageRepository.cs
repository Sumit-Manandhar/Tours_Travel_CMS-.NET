using vHolidays.DataAccess.Repository.Package.Interface;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;

namespace vHolidays.DataAccess.Repository.Package.Repository
{
    public class PackageImageRepository : Repository<PackageImage>, IPackageImageRepository
    {
        private readonly ApplicationDbContext _db;
        public PackageImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<string>> SaveImages(int Id, List<string> urls, int[]? todelete)
        {
            if( (urls is null || !urls.Any()) && (todelete is null|| todelete.Count()==0))
                return new ResponseModel<string>()
                {
                    Message = "No image found to upload.",
                    Succeeded = false
                };

            try
            {
                foreach (var url in urls)
                {
                    Add(new PackageImage()
                    {
                        ImageUrl = url,
                        PackageDetailId = Id,
                    });
                }
                if (todelete is not null && todelete.Any())
                {
                    _db.PackageImage.RemoveRange(_db.PackageImage.Where(a => todelete.Contains(a.Id)));
                }
                await _db.SaveChangesAsync();
                return new ResponseModel<string>()
                {
                    Message = "Images Saved.",
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>()
                {
                    Message = ex.Message,
                    Succeeded = false
                };
            }

        }
    }
}
