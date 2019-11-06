using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Xml;
using Ocelot.Responses;
using Xunit;

using Products.Controllers;
using Products.Services;
using Products.Domain;
using Products.Domain.Models;
using Products.Domain.Repositories;
using Products.Domain.Services;
using Products.Mapping;
using Products.Persistence.Repositories;
using Products.Resources;


namespace Services.Tests.WebServices.Products.Controllers
{
    public class ProductsControllerTest
    {
//        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        
        public ProductsControllerTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ModelToResourceProfile()));
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            // Arrange
            var service = new Mock<IProductService>();
            var products = GetTestProducts();
            service.Setup(e => e.ListAsync()).ReturnsAsync(products);
            var controller = new ProductsController(service.Object, _mapper);

            //Act
            var result = await controller.GetAllAsync();
            
            //Assert
            var actionResult = Assert.IsType<List<ProductResource>>(result);
            var models = Assert.IsAssignableFrom<List<ProductResource>>(result);
            Assert.Equal(3, models.Count());
        }

        private List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product()
                {
                    Id = 1000,
                    Name = "FProd",
                    Cost = 10000
                },
                new Product()
                {
                    Id = 1001,
                    Name = "SProd",
                    Cost = 20000
                },
                new Product()
                {
                    Id = 1002,
                    Name = "TProd",
                    Cost = 30000
                }
            };
        }

    }
}