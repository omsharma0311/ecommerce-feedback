using AutoMapper;
using ECommerceFeedback.Common;
using ECommerceFeedback.Facade.Products;
using ECommerceFeedback.Models.Data;
using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Repository.ProductRepository;
using Moq;

namespace ECommerceFeedback.Test
{
    [TestFixture]
    public class ProductsTests
    {
        private IProductsFacade _productsFacade;
        private Mock<IProductRepository> _mockProductRepository;

        [SetUp]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = new Mapper(configuration);
            _mockProductRepository = new Mock<IProductRepository>();
            _productsFacade = new ProductsFacade(_mockProductRepository.Object, mapper);
        }

        [Test]
        public async Task AddProductSuccessfully()
        {
            // Arrange
            var productRequest = new ProductRequest()
            {
                Name = "bag",
                Description = "bag",
                Price = 10,
                Category = "bag"
            };
            _mockProductRepository.Setup(u => u.AddProducts(It.IsAny<Product>())).ReturnsAsync(new Product()
            {
                Name = "socks",
                Description = "socks",
                Price = 100,
                Category = "socks"
            });

            // Act
            var response = await _productsFacade.AddProduct(productRequest);

            // Assert
            Assert.IsNotNull(response.Products);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task GetProductsListingRequest()
        {
            // Arrange
            var productsListingRequest = new ProductsListingRequest()
            {
                Category = "cloths",
                PriceOrderBy = "low to high"
            };

            _mockProductRepository.Setup(u => u.GetAllProducts(It.IsAny<List<Filter>>())).ReturnsAsync(new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "jacket",
                    Description = "jacket",
                    Price = 2500,
                    Category = "cloths",
                    ExpiryDate = DateTime.Parse("2024-05-14T21:14:20.872Z")
                }
            });

            // Act
            var response = await _productsFacade.ProductsListing(productsListingRequest);

            // Assert
            Assert.IsNotNull(response.Products);
            Assert.AreEqual(1, response.Products.Count);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public async Task GetProductsDetails()
        {
            // Arrange
            long userId = 1;

            _mockProductRepository.Setup(u => u.ProductDetails(It.IsAny<long>())).ReturnsAsync(new Product()
            {
                    ProductId = 1,
                    Name = "jacket",
                    Description = "jacket",
                    Price = 2500,
                    Category = "cloths",
                    ExpiryDate = DateTime.Parse("2024-05-14T21:14:20.872Z")                
            });

            // Act
            var response = await _productsFacade.ProductDetails(userId);

            // Assert
            Assert.IsNotNull(response.Products);
            Assert.IsTrue(response.Success);
        }

    }
}

