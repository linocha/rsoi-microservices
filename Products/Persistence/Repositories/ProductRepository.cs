using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Models;
using Products.Domain.Repositories;
using Products.Persistence.Contexts;

namespace Products.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}