using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Models;
using Travels.Models.ViewModels;

namespace Travels.DataAccess.Repository.IRepository
{
    public interface ISubdestinationsRepository :  IRepository<Subdestinations>
    {
        Task Update(SaveSubdestinationModel obj);
        Task Add(SaveSubdestinationModel obj);
        SubdestinationDetailsModel GetById(int Id);
        Task OnOffDelete(int SubdestinationId);
    }
}
