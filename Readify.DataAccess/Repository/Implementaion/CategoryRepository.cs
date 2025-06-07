using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;

namespace Readify.DataAccess.Repository.Implementaion
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
