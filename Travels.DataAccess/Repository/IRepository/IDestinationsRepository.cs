using Travels.Models;
using Travels.Models.ViewModels;

namespace Travels.DataAccess.Repository.IRepository
{
	public interface IDestinationsRepository : IRepository<Destination>
    {
        Task Update(SaveDestinationModel obj);
        Task<int> Add(SaveDestinationModel obj);

        DestinationDetailsModel GetById(int Id);
        Task OnOffDelete(int DestinationId);

    }

}
