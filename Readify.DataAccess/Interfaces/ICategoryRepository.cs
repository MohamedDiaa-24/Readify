using Readify.DataAccess.Repository.IRepository;
using Readify.Models;

namespace Readify.DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Save();
        void Update(Category category);
    }
}
