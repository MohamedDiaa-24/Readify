using Readify.DataAccess.Repository.Interfaces.IRepository;
using Readify.Models;

namespace Readify.DataAccess.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
