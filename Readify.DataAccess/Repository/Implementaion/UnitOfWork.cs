using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.Interfaces;

namespace Readify.DataAccess.Repository.Implementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        private ApplicationDBContext _dbContext;
        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            Category = new CategoryRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
