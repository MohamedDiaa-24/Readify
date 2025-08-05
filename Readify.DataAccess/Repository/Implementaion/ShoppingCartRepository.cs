using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;

namespace Readify.DataAccess.Repository.Implementaion
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDBContext _dbContext;
        public ShoppingCartRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }



        public void Update(ShoppingCart shoppingCart)
        {
            _dbContext.Set<ShoppingCart>().Update(shoppingCart);
        }
    }
}
