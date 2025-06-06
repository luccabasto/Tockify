using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IAdmUserRepository
    {
        Task<IEnumerable<AdmUserModel>> GetAllUserEmailsAsync();
        Task<TaskItemModel> IsEmailRegisteredAsync(string email);
        Task<TaskItemModel> IsUserActiveAsync(Guid userId);
        Task ActivateUserAsync(Guid userId);
        Task DeactivateUserAsync(Guid userId);
        Task DeleteUserAsync(Guid userId);
        Task UpdateUserProfileAsync(Guid userId, string profileData);
        Task<string?> GetUserProfileAsync(Guid userId);
    }
}
