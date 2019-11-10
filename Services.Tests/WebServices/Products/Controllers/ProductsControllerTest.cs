using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Xunit;

using Products.Controllers;
using Products.Domain.Models;
using Products.Domain.Services;
using Products.Domain.Services.Communication;
using Products.Mapping;
using Products.Resources;

using Services.Tests.Utils.AssertHelpers;
using Services.Tests.Utils.Generators;


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
            //Arrange
            var service = new Mock<IProductService>();
            var products = ProductGenerator.GetTestProducts();
            service.Setup(e => e.ListAsync()).ReturnsAsync(products);
            
            var controller = new ProductsController(service.Object, _mapper);

            //Act
            var result = await controller.GetAllAsync();
            
            //Assert
            var actionResult = Assert.IsType<List<ProductResource>>(result);
            var productResources = Assert.IsAssignableFrom<List<ProductResource>>(actionResult);
            AssertHelperBase.AssertEqualLists(products, actionResult, ProductAssertHelper.AssertEquals);
            Assert.Equal(3, productResources.Count());
        }

        
        [Fact]
        public async Task GetByIdOkTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var productResponse = ProductGenerator.GetTestProductResponse();
            service.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productResponse);
            
            var controller = new ProductsController(service.Object, _mapper);
            
            //Act
            var result = await controller.GetById(productResponse.Product.Id);
            
            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var productResource = Assert.IsAssignableFrom<ProductResource>(actionResult.Value);
            ProductAssertHelper.AssertEquals(productResponse.Product, productResource);
        }

        
        [Fact]
        public async Task GetByIdNotFoundTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var productResponse = new ProductResponse("Product not found");
            service.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productResponse);
            
            var controller = new ProductsController(service.Object, _mapper);
            
            //Act
            var result = await controller.GetById(1);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        
        [Fact]
        public async Task PostAsyncOkTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var saveProductResource = ProductGenerator.GetTestSaveProductResource();

            var product = ProductGenerator.GetTestProduct();
            product.Name = saveProductResource.Name;
            product.Cost = saveProductResource.Cost;

            var productResponse = new ProductResponse(product);
            service.Setup(e => e.SaveAsync(It.IsAny<Product>())).ReturnsAsync(productResponse);
            
            var controller = new ProductsController(service.Object, _mapper);

            //Act
            var result = await controller.PostAsync(saveProductResource);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var productResource = Assert.IsAssignableFrom<ProductResource>(actionResult.Value);
            
            ProductAssertHelper.AssertEquals(saveProductResource, productResource);
        }
        

    }
}