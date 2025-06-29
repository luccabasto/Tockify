using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface IGetClientUserByIdCase
    {
        Task<ClientUserDto> GetUserByIdAsync(int userId);
    }
}
