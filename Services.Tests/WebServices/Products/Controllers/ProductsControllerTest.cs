using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ProductDtoMappingProfile()));
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
            var saveProductResource = ProductGenerator.GetTestSaveProductResource();

            var product = ProductGenerator.GetTestProduct();
            product.Name = saveProductResource.Name;
            product.Cost = saveProductResource.Cost;

            var productResponse = new ProductResponse(product);
            
            var service = new Mock<IProductService>();
            service.Setup(e => e.SaveAsync(It.IsAny<Product>())).ReturnsAsync(productResponse);
            
            var controller = new ProductsController(service.Object, _mapper);

            //Act
            var result = await controller.PostAsync(saveProductResource);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var productResource = Assert.IsAssignableFrom<ProductResource>(actionResult.Value);
            
            ProductAssertHelper.AssertEquals(saveProductResource, productResource);
        }


        [Fact]
        public async Task PostAsyncBadRequestTest()
        {
            // Arrange
            var controller = new ProductsController(Mock.Of<IProductService>(), _mapper);
            controller.ModelState.AddModelError("error", "some error");
            
            // Act
            var result = await controller.PostAsync(ProductGenerator.GetTestSaveProductResource());
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        
        [Fact]
        public async Task PutAsyncOkTest()
        {
            //Arrange
            var saveProductResource = ProductGenerator.GetTestSaveProductResource();
            
            var product = ProductGenerator.GetTestProduct();
            product.Name = saveProductResource.Name;
            product.Cost = saveProductResource.Cost;
            
            var productResponse = new ProductResponse(product);
            
            var service = new Mock<IProductService>();
            service.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productResponse);
            service.Setup(e => e.UpdateAsync(It.IsAny<int>(), It.IsAny<Product>())).ReturnsAsync(productResponse);
            
            var controller = new ProductsController(service.Object, _mapper);

            //Act
            var result = await controller.PutAsync(1, saveProductResource);
            
            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var productResource = Assert.IsAssignableFrom<ProductResource>(actionResult.Value);
            
            ProductAssertHelper.AssertEquals(saveProductResource, productResource);
        }

        
        [Fact]
        public async Task PutAsyncBadRequestTest()
        {
            // Arrange
            var controller = new ProductsController(Mock.Of<IProductService>(), _mapper);
            controller.ModelState.AddModelError("error", "some error");
            
            // Act
            var result = await controller.PutAsync(1, ProductGenerator.GetTestSaveProductResource());
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        
        [Fact]
        public async Task DeleteAsyncOkTest()
        {
            var product = ProductGenerator.GetTestProduct();
            var productResponse = new ProductResponse(product);
            
            var service = new Mock<IProductService>();
            service.Setup(e => e.DeleteAsync(It.IsAny<int>())).ReturnsAsync(productResponse);
            
            var controller = new ProductsController(service.Object, _mapper);
            
            // Act
            var result = await controller.DeleteAsync(1);
            
            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var productResource = Assert.IsAssignableFrom<ProductResource>(actionResult.Value);
            
            ProductAssertHelper.AssertEquals(productResponse, productResource);
        }

        
        [Fact]
        public async Task DeleteAsyncNotFoundTest()
        {
            // Arrange
            var productResponse = new ProductResponse("error");
            
            var service = new Mock<IProductService>();
            service.Setup(e => e.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productResponse);
            service.Setup(e => e.DeleteAsync(It.IsAny<int>())).ReturnsAsync(productResponse);
            
            var controller = new ProductsController(service.Object, _mapper);
            
            // Act
            var result = await controller.DeleteAsync(1);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}