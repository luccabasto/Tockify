using Tockify.Domain.Enums;
using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IClientUserRepository
    {
        Task<ClientUserModel> RegisterUserAsync(ClientUserModel user, string email, string password);
        Task<List<ClientUserModel>> GetAllClientUsersAsync(UserProfile profile, bool? isActive = null, string? name = null);
        Task<ClientUserModel?> GetUserByIdAsync(int id);
        Task<ClientUserModel?> GetUserByEmailAsync(string email);
        Task<ClientUserModel> UpdateClientUserByIdAsync(ClientUserModel user, string email, string password);
        Task<ClientUserModel> DeleteClientUserByIdAsync(int id);
        Task<bool> ClientUserExistsAsync(string email);
        Task? IncrementIncompleteCountAsync(int userId);
        Task? DecrementIncompleteCountAsync(int userId);

        Task<(List<ClientUserModel> Users, long Total)
            >GetPagedAsync(UserProfile profile, bool? isActive, int skip, int take);
    }
}
