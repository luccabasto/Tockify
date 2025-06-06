using Tockify.Domain.Enums;
using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IClientUserRepository
    {
        Task<List<ClientUserModel>> GetAllClientUsers(UserProfile profile);
        Task<ClientUserModel?> GetUserByIdAsync(Guid id);
        Task<ClientUserModel?> GetUserByEmailAsync(string email);
        Task<ClientUserModel> RegisterUserAsync(ClientUserModel user, string email, string password);
        Task<ClientUserModel> UpdateClientUserByIdAsync(ClientUserModel user, string email, string password);
        Task<bool> DeleteClientUserByIdAsync(Guid id);
        Task<bool> ClientUserExistsAsync(string email);
    }
}
