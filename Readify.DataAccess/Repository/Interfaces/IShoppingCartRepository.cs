using Readify.DataAccess.Repository.Interfaces.IRepository;
using Readify.Models;

namespace Readify.DataAccess.Repository.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shoppingCart);
    }
}
