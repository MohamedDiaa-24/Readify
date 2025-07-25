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
            var productFromDB = _dBContext.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productFromDB != null)
            {
                productFromDB.ISBN = product.ISBN;
                productFromDB.ListPrice = product.ListPrice;
                productFromDB.Price = product.Price;
                productFromDB.Price50 = product.Price50;
                //productFromDB.Category.Id = product.Category.Id;
                productFromDB.CategoryID = product.CategoryID;
                productFromDB.ListPrice = product.ListPrice;
                productFromDB.Author = product.Author;
                productFromDB.Description = product.Description;
                if (product.ImageURL != null)
                {

                    productFromDB.ImageURL = product.ImageURL;
                }
            }
        }
    }
}
