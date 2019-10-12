using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Domain.Models;

namespace Products.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
    }
}