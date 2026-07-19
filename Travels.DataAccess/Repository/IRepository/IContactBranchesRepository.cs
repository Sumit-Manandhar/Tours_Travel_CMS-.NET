using Travels.Models.Settings;
using Travels.Models.Settings.DataTransferObjects;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface IContactBranchesRepository :IRepository<ContactBranches>
    {
        Task<List<ContactBranches>> GetAll();
        Task Update(ContactBranches contactBranches);
        Task<int> AddUpdate(BranchesDTO model);
        Task OnOffDelete(int DestinationId);
    }
}
