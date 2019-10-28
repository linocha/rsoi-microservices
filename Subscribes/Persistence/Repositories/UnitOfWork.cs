using System.Threading.Tasks;
using Subscribes.Domain.Repositories;
using Subscribes.Persistence.Contexts;

namespace Subscribes.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        
        public UnitOfWork(AppDbContext context)
        {
            _context = context;     
        }
        
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}