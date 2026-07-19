using System.Linq;
using vHolidays.DataAccess.Repository.Package.Interface;
using vHolidays.DataAcess.Data;
using vHolidays.Models.Common;
using vHolidays.Models.Package.DatabaseModel;
using vHolidays.Models.Package.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.Package.Repository
{
    public class PackageGroupRepository : Repository<PackageGroup>, IPackageGroupRepository
    {
        private readonly ApplicationDbContext _db;

        public PackageGroupRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ResponseModel<int>> CreateUpdate(GroupAddUpdateModel model)
        {
            try
            {
                PackageGroup data = new()
                {
                    Id = model.Id,
                    Adult = model.Adult,
                    FreeOfCost = model.FreeOfCost,
                    SingleSupliment = model.SingleSupliment,
                    Order = model.Order,
                    PackageDetailId = model.PackageDetailId,
                    IsActive = model.IsActive,
                    IsPublished = model.IsPublished,
                    IsDeleted = model.IsDeleted,
                };
                if (model.Order == 0)
                {
                    var maxOrder = _db.PackageGroup.Where(x => x.PackageDetailId == model.PackageDetailId);
                    data.Order = maxOrder is null|| !maxOrder.Any() ? 1 : maxOrder.Max(x => x.Order) + 1;
                }

                Update(data);
                await _db.SaveChangesAsync();
                return new ResponseModel<int>
                {
                    Data = data.Id,
                    Succeeded = true,
                    Message = "Group Saved",
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

        public async Task<GroupAddUpdateModel> Get(int Id)
        {
            var model = Get(x => x.Id == Id);
            return new GroupAddUpdateModel()
            {
                Id = Id,
                Adult = model.Adult,
                FreeOfCost = model.FreeOfCost,
                SingleSupliment = model.SingleSupliment,
                PackageDetailId = model.PackageDetailId,
                Order = model.Order,
                IsActive = model.IsActive,
                IsPublished = model.IsPublished,
                IsDeleted = model.IsDeleted,
            };
        }

        public async Task<ResponseModel<string>> UpdateOrder(List<DataOrderingModel> model)
        {
            try
            {
                foreach (var item in model)
                {
                    var data = Get(x => x.Id == item.Id);
                    data.Order = item.Ordering;
                    Update(data);
                }

                await _db.SaveChangesAsync();
                return new ResponseModel<string>
                {
                    Succeeded = true,
                    Message = "Order Saved",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    Succeeded = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
