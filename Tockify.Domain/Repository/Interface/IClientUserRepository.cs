using Tockify.Domain.Enums;
using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IClientUserRepository
    {
        // Methods for managing client users
        Task<List<ClientUserModel>> GetAllClientUsers(UserProfile profile);
        Task<ClientUserModel?> GetUserByIdAsync(Guid id);
        Task<ClientUserModel?> GetUserByEmailAsync(string email);

        // CRUD operations for client users
        Task<ClientUserModel> RegisterUserAsync(ClientUserModel user, string email, string password);
        Task<ClientUserModel> UpdateClientUserByIdAsync(ClientUserModel user, Guid id);
        Task<bool> DeleteClientUserByIdAsync(ClientUserModel user, Guid id);

        // Additional methods for user management
        Task<bool> ClientUserExistsAsync(ClientUserModel user, Guid id);
    }
}
