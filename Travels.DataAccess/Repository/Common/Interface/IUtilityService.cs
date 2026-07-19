using Microsoft.AspNetCore.Mvc.Rendering;

namespace Travels.DataAccess.Repository.Common.Interface
{
    public interface IUtilityService
    {
        Task<List<SelectListItem>> GetMembercount(bool isAdult);
        Task<List<SelectListItem>> GetSalutation();
        Task<List<SelectListItem>> GetProductSupplies();
        string GetProductSuppliesName(int Id);
    }
}
