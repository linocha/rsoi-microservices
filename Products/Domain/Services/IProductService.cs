using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Domain.Models;
using Products.Domain.Services.Communication;

namespace Products.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<SaveProductResponse> SaveAsync(Product product);
    }
}