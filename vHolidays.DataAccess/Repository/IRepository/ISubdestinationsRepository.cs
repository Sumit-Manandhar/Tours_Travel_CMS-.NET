using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vHolidays.Models;
using vHolidays.Models.ViewModels;

namespace vHolidays.DataAccess.Repository.IRepository
{
    public interface ISubdestinationsRepository :  IRepository<Subdestinations>
    {
        Task Update(SaveSubdestinationModel obj);
        Task Add(SaveSubdestinationModel obj);
        SubdestinationDetailsModel GetById(int Id);
        Task OnOffDelete(int SubdestinationId);
    }
}
