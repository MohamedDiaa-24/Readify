using Microsoft.EntityFrameworkCore;
using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.Interfaces.IRepository;

namespace Readify.DataAccess.Repository.Implementaion
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;

        public Repository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            _dbContext.Products.Include(p => p.Category);
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {

                foreach (var prop in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (!string.IsNullOrEmpty(includeProperties))
            {

                foreach (var prop in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }
    }
}
