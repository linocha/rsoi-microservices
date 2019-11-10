using Products.Domain.Models;
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
    }
}