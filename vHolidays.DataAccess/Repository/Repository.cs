using System.Linq.Expressions;
using vHolidays.DataAccess.Repository.IRepository;
using vHolidays.DataAcess.Data;
using Microsoft.EntityFrameworkCore;

namespace vHolidays.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();
		}

		public void Add(T entity)
		{
			dbSet.Add(entity);
		}
        public void Update(T entity) => dbSet.Update(entity);

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
		{
			IQueryable<T> query;
			if (tracked)
			{
				query = dbSet;

			}
			else
			{
				query = dbSet.AsNoTracking();
			}

			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.FirstOrDefault();

		}

		public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
				query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			return query;
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}
