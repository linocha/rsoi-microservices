using System.Collections.Generic;
using System.Threading.Tasks;
using Subscribes.Domain.Models;

namespace Subscribes.Domain.Repositories
{
    public interface ISubscribeRepository
    {
        Task<IEnumerable<Subscribe>> ListAsync();
        Task AddAsync(Subscribe subscribe);
        Task<Subscribe> FindByIdAsync(int id);
        void Update(Subscribe subscribe);
        void Remove(Subscribe subscribe);
    }
}