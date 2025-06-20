using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface IGetAllClientUsersCase
    {
        Task<IEnumerable<ClientUserDto>> GetAllClient();
    }
}
