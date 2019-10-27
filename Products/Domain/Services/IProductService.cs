using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Domain.Models;
using Products.Domain.Services.Communication;

namespace Products.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> GetByIdAsync(int id);

        Task<ProductResponse> DeleteAsync(int id);
    }
}