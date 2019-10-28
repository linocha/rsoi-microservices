using System.Collections.Generic;
using System.Threading.Tasks;
using Subscribes.Domain.Models;
using Subscribes.Domain.Services.Communication;

namespace Subscribes.Domain.Services
{
    public interface ISubscribeService
    {
        Task<IEnumerable<Subscribe>> ListAsync();
        Task<SubscribeResponse> SaveAsync(Subscribe subscribe);
//        Task<SubscribeResponse> UpdateAsync(int id, Subscribe subscribe);
        Task<SubscribeResponse> GetByIdAsync(int id);

        Task<SubscribeResponse> DeleteAsync(int id);

        Task<SubscribeResponse> UpdateAsync(int id);
    }
}