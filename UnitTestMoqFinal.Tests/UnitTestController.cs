using Moq;
using UnitTestMoqFinal.Controllers;
using UnitTestMoqFinal.Models;
using UnitTestMoqFinal.Services;

namespace UnitTestProject
{
    public class UnitTestController
    {
        private readonly Mock<IProductService> productService;

        public UnitTestController()
        {
            productService = new Mock<IProductService>();
        }

        private List<Product> GetProductsData()
        {
            List<Product> productsData =
            [
                new Product
                {
                    ProductId = 1,
                    ProductName = "iPhone",
                    ProductDescription = "IPhone 12",
                    ProductPrice = 55000,
                    ProductStock = 10
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Laptop",
                    ProductDescription = "HP Pavilion",
                    ProductPrice = 100000,
                    ProductStock = 20
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "TV",
                    ProductDescription = "Samsung Smart TV",
                    ProductPrice = 35000,
                    ProductStock = 30
                },
            ];
            return productsData;
        }

        [Fact]
        public void GetProductList_ProductList()
        {
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductList()).Returns(productList);
            var productController = new ProductController(productService.Object);

            var productResult = productController.ProductList();

            Assert.NotNull(productResult);
            Assert.Equal(3, GetProductsData().Count);
            Assert.Equal(GetProductsData().ToString(), productResult.ToString());
            Assert.True(productList.Equals(productResult));
        }

        [Fact]
        public void GetProductById_Product()
        {
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductById(1)).Returns(productList[0]);
            var productController = new ProductController(productService.Object);

            var productResult = productController.GetProductById(1);

            Assert.NotNull(productResult);
            Assert.Equal(productList[0].ProductId, productResult.ProductId);
            Assert.True(productList[0].ProductId == productResult.ProductId);
        }

        [Theory]
        [InlineData("iPhone")]
        public void CheckProductExistOrNotByProductName_Product(string productName)
        {
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductList()).Returns(productList);
            var productController = new ProductController(productService.Object);

            var productResult = productController.ProductList();
            var productMatch = productResult.Where(x => x.ProductName == productName);
            Console.WriteLine("Product: {0}", productMatch.ToList()[0].ProductName);

            Assert.NotNull(productMatch);
            Assert.Single(productMatch);
        }

        [Fact]
        public void AddProduct_Product()
        {
            var productToAdd = new Product
            {
                ProductId = 4,
                ProductName = "Macbook",
                ProductDescription = "Laptop",
                ProductPrice = 1000,
                ProductStock = 18
            };
            var productList = GetProductsData();
            productService
                .Setup(x => x.AddProduct(productToAdd))
                .Callback(() => productList.Add(productToAdd))
                .Returns(productToAdd);
            var productController = new ProductController(productService.Object);

            var productResult = productController.AddProduct(productToAdd);

            Console.WriteLine("Products: {0}", productList.Count);

            Assert.NotNull(productResult);
            Assert.Equal(productToAdd.ProductId, productResult.ProductId);
            Assert.True(productToAdd.ProductId == productResult.ProductId);
        }
    }
}
