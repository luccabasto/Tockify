using Tockify.Application.DTOs;
using Tockify.Domain.Enums;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface IGetAllClientUsersCase
    {
        Task<List<ClientUserDto>> GetAllClient(UserProfile? profile, bool? isActive, string? name);
    }
}
