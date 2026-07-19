using Travels.Models.Settings;
using Travels.Models.Settings.DataTransferObjects;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface IBranchAgentsRepository : IRepository<BranchAgents>
    {
        Task<List<BranchAgents>> GetAll(int id);

        Task<int> AddUpdate(AgentsDTO Model);

        Task<AgentsDTO> Get(int id);
        Task OnOffDelete(int id);
    }
}
