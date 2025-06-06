using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IUserAdmRepository
    {
        Task<IEnumerable<UserAdmModel>> GetAllUserEmailsAsync();
        Task<TaskItemModel> IsEmailRegisteredAsync(string email);
        Task<TaskItemModel> IsUserActiveAsync(Guid userId);
        Task ActivateUserAsync(Guid userId);
        Task DeactivateUserAsync(Guid userId);
        Task DeleteUserAsync(Guid userId);
        Task UpdateUserProfileAsync(Guid userId, string profileData);
        Task<string?> GetUserProfileAsync(Guid userId);
    }
}
