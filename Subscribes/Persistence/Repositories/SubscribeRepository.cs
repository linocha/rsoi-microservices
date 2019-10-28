using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Subscribes.Domain.Models;
using Subscribes.Domain.Repositories;
using Subscribes.Persistence.Contexts;

namespace Subscribes.Persistence.Repositories
{
    public class SubscribeRepository : BaseRepository, ISubscribeRepository
    {
        public SubscribeRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Subscribe>> ListAsync()
        {
            return await _context.Subscribes.ToListAsync();
        }

        public async Task AddAsync(Subscribe subscribe)
        {
            await _context.Subscribes.AddAsync(subscribe);
        }

        public async Task<Subscribe> FindByIdAsync(int id)
        {
            return await _context.Subscribes.FindAsync(id);
        }

        public void Update(Subscribe subscribe)
        {
            _context.Subscribes.Update(subscribe);
        }

        public void Remove(Subscribe subscribe)
        {
            _context.Subscribes.Remove(subscribe);
        }
    }
}