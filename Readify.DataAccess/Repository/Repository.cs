using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.IRepository;

namespace Readify.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;

        public Repository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
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
