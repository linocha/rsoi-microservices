using System.Collections.Generic;
using Products.Domain.Models;
using Products.Domain.Services.Communication;
using Products.Resources;

namespace Services.Tests.Utils.Generators
{
    public class ProductGenerator
    {
        public static List<Product> GetTestProducts()
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

        public static Product GetTestProduct()
        {
            return new Product()
            {
                Id = 1000,
                Name = "Prod",
                Cost = 1000
            };
        }
        
        public static ProductResponse GetTestProductResponse()
        {
            return new ProductResponse(new Product()
            {
                Id = 1000,
                Name = "FProd",
                Cost = 10000
            });
        }

        public static ProductResource GetTestProductResource()
        {
            return new ProductResource()
            {
                Id = 1000,
                Name = "Prod",
                Cost = 1000
            };
        }

        public static SaveProductResource GetTestSaveProductResource()
        {
            return new SaveProductResource()
            {
                Name = "Prod",
                Cost = 1000
            };
        }
    }
}