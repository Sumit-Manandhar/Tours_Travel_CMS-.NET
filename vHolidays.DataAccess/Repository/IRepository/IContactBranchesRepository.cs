using vHolidays.Models.Settings;
using vHolidays.Models.Settings.DataTransferObjects;

namespace vHolidays.DataAccess.Repository.IRepository
{
    public interface IContactBranchesRepository :IRepository<ContactBranches>
    {
        Task<List<ContactBranches>> GetAll();
        Task Update(ContactBranches contactBranches);
        Task<int> AddUpdate(BranchesDTO model);
        Task OnOffDelete(int DestinationId);
    }
}
