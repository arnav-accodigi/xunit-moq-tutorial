using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitTestMoqFinal.Models;
using UnitTestMoqFinal.Services;

namespace UnitTestMoqFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("productlist")]
        public IEnumerable<Product> ProductList()
        {
            return _productService.GetProductList();
        }

        [HttpGet("getproductbyid")]
        public Product? GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost("addproduct")]
        public Product AddProduct(Product product)
        {
            return _productService.AddProduct(product);
        }

        [HttpPut("updateproduct")]
        public Product UpdateProduct(Product product)
        {
            return _productService.UpdateProduct(product);
        }

        [HttpDelete("deleteproduct")]
        public bool DeleteProduct(int id)
        {
            return _productService.DeleteProduct(id);
        }
    }
}
