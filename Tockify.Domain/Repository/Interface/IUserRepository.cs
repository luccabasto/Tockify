using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUserByEmailAsync(string email);
        Task<UserModel?> GetUserByIdAsync(Guid id);
        Task<UserModel?> GetUserByNameAsync(string name);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task AddUserAsync(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(Guid id);
        Task<bool> UserExistsAsync(string email);
        Task AddUserAsync(DomainService.UserModel user);
    }
}
