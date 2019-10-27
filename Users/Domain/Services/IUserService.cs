using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Domain.Models;
using Users.Domain.Services.Communication;

namespace Users.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(int id, User user);
        Task<UserResponse> GetByIdAsync(int id);

        Task<UserResponse> DeleteAsync(int id);
    }
}