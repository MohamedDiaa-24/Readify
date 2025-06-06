using Readify.DataAccess.Data;
using Readify.DataAccess.Interfaces;
using Readify.DataAccess.Repository;
using Readify.Models;

namespace Readify.DataAccess.Implementaion
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDBContext _dbContext;
        public CategoryRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _dbContext.Set<Category>().Update(category);
        }
    }
}
