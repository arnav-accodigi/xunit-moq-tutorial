using UnitTestMoqFinal.Data;
using UnitTestMoqFinal.Models;

namespace UnitTestMoqFinal.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContext;

        public ProductService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetProductList()
        {
            return _dbContext.Products.ToList();
        }

        public Product? GetProductById(int id)
        {
            return _dbContext.Products.Where(product => product.ProductId == id).FirstOrDefault();
        }

        public Product AddProduct(Product product)
        {
            var result = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return result.Entity;
        }

        public Product UpdateProduct(Product product)
        {
            var result = _dbContext.Products.Update(product);
            _dbContext.SaveChanges();

            return result.Entity;
        }

        public bool DeleteProduct(int id)
        {
            Product? productToDelete = _dbContext
                .Products.Where(product => product.ProductId == id)
                .FirstOrDefault();

            if (productToDelete == null)
            {
                Console.WriteLine("Error: Product not found.");
                return false;
            }

            var result = _dbContext.Products.Remove(productToDelete);
            _dbContext.SaveChanges();

            return result != null;
        }
    }
}
