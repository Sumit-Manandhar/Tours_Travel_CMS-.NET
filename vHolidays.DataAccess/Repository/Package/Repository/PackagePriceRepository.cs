using vHolidays.DataAccess.Repository.Package.Interface;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Repository
{
    public class PackagePriceRepository : Repository<PackagePrice>, IPackagePriceRepository
    {
        private readonly ApplicationDbContext _db;

        public PackagePriceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<PriceViewModel> GetAll(int Id, bool AllPrice)
        {
            await UpdateClassOptionAndGroup(Id);
            var priceData = _db.PackagePrice.Where(x => x.PackageDetailId == Id && x.IsActive && x.IsPublished && !x.IsDeleted).Select(x => new PackagePriceDto()
            {
                Id = x.Id,
                PackageClassOptionId = x.PackageClassOptionId,
                PackageDetailId = x.PackageDetailId,
                PackageGroupId = x.PackageGroupId,
                Price = x.Price
            });
            var classOption = _db.PackageClassOption.Where(x => x.PackageDetailId == Id && x.IsSelected).Select(x => new PackageClassOptionDto()
            {
                Id = x.Id,
                Name = x.Name,
                PackageDetailId = x.PackageDetailId,
                IsSelected = x.IsSelected,
            });
            var groupList = _db.PackageGroup.Where(x => x.PackageDetailId == Id && x.IsActive && x.IsPublished && !x.IsDeleted).Select(x => new PackageGroupDto()
            {
                Id = x.Id,
                Adult = x.Adult,
                PackageDetailId = x.PackageDetailId,
                FreeOfCost = x.FreeOfCost,
                SingleSupliment = x.SingleSupliment,
                Order = x.Order,
            });
            var priceList = AllPrice ? priceData.ToList() : priceData.Where(x => x.Price > 0).ToList();
            var finalClassOption = AllPrice ? classOption.ToList() : classOption.AsEnumerable()
                .Where(x => priceList.Any(y => x.Id == y.PackageClassOptionId))
                .ToList();

            var finalGroupList = AllPrice ? groupList.ToList() : groupList.AsEnumerable()
                .Where(x => priceList.Any(y => x.Id == y.PackageGroupId))
                .ToList();
            return new PriceViewModel()
            {
                PriceList = priceList,
                ClassOption = finalClassOption,
                GroupList = finalGroupList
            };
        }

        public async Task<ResponseModel<string>> Update(List<PackagePriceDto> model)
        {
            try
            {
                foreach (var item in model)
                {
                    var data = Get(x => x.Id == item.Id);
                    data.Price = item.Price;
                    _db.PackagePrice.Update(data);
                }
                await _db.SaveChangesAsync();
                return new ResponseModel<string>()
                {
                    Succeeded = true,
                    Message = "Price saved."
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

        public async Task UpdateClassOptionAndGroup(int Id)
        {
            var pacakgeList = _db.PackageClassOption.Where(x => x.PackageDetailId == Id).ToList();
            var groupList = _db.PackageGroup.Where(x => x.PackageDetailId == Id).ToList();
            foreach (var pkg in pacakgeList)
            {
                foreach (var grp in groupList)
                {
                    var data = Get(x => x.PackageClassOptionId == pkg.Id && x.PackageGroupId == grp.Id && x.PackageDetailId == Id);
                    if (data is null)
                    {
                        _db.PackagePrice.Add(new PackagePrice() { PackageGroupId = grp.Id, PackageClassOptionId = pkg.Id, PackageDetailId = Id, IsActive = true, IsPublished = true });
                    }
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
