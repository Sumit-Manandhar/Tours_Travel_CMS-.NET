using System.Linq.Expressions;
using Travels.Models.Base;

namespace Travels.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
    {
        IQueryable<T>  GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
