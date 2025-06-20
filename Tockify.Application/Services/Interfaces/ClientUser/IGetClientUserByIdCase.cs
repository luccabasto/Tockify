using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface IGetClientUserByIdCase
    {
        Task<IEnumerable<ClientUserDto>> GetByIdAsync(int id);
    }
}
