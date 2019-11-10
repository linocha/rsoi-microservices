using Products.Domain.Models;
using Products.Domain.Services.Communication;
using Products.Resources;
using Xunit;

namespace Services.Tests.Utils.AssertHelpers
{
    public class ProductAssertHelper : AssertHelperBase
    {
        public static void AssertEquals(Product product, ProductResource productResource)
        {
            Assert.Equal(product.Id, productResource.Id);
            Assert.Equal(product.Name, productResource.Name);
            Assert.Equal(product.Cost, productResource.Cost);
        }

        public static void AssertEquals(SaveProductResource saveProductResource, ProductResource productResource)
        {
            Assert.Equal(saveProductResource.Name, productResource.Name);
            Assert.Equal(saveProductResource.Cost, productResource.Cost);
        }
        
        public static void AssertEquals(ProductResponse productResponse, ProductResource productResource)
        {
            Assert.Equal(productResponse.Product.Name, productResource.Name);
            Assert.Equal(productResponse.Product.Cost, productResource.Cost);
        }
    }
}