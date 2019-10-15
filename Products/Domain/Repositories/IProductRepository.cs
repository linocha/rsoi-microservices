using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Domain.Models;

namespace Products.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task AddAsync(Product product);
    }
}