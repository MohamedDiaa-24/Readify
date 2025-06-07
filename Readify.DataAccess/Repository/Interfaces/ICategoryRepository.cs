using Readify.DataAccess.Repository.Interfaces.IRepository;
using Readify.Models;

namespace Readify.DataAccess.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
