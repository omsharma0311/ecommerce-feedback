using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using ECommerceFeedback.Common;
using ECommerceFeedback.Facade.Shopping;
using ECommerceFeedback.Models.Data;
using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Repository.ProductRepository;
using ECommerceFeedback.Repository.ShoppingRepository;
using Moq;
using NUnit.Framework;



namespace ECommerceFeedback.Test
{
    [TestFixture]
    public class ShoppingCartTest
    {
        private IShoppingFacade _shoppingFacade;
        private Mock<IShoppingRepository> _mockShoppingRepository;
        private Mock<IProductRepository> _mockProductRepository;

        [SetUp]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = new Mapper(configuration);
            _mockShoppingRepository = new Mock<IShoppingRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _shoppingFacade = new ShoppingFacade(_mockShoppingRepository.Object, _mockProductRepository.Object, mapper);
        }

        [Test]
        public async Task AddProductInBagSuccessfully()
        {
            // Arrange
            var bagRequest = new BagRequest()
            {
                UserId = 1,
                ProductInBag = new List<ProductInBag>()
                {
                    new ProductInBag()
                    {
                        ProductId = 1,
                        Quantity = 2
                    }
                }
            };
            _mockShoppingRepository.Setup(u => u.AddProductsToBag(It.IsAny<UserCart>())).ReturnsAsync(true);

            // Act
            var response = await _shoppingFacade.AddProductsToBag(bagRequest);

            // Assert
            Assert.AreEqual("Products added successfully in cart.", response.Text);
            Assert.IsTrue(response.Success);
        }

    }
        
}
