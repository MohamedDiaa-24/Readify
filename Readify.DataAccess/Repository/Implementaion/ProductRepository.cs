using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;

namespace Readify.DataAccess.Repository.Implementaion
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _dBContext;
        public ProductRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;

        }

        public void Update(Product product)
        {
            _dBContext.Products.Update(product);
        }
    }
}
