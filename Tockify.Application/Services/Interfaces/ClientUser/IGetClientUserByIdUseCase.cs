using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface IGetClientUserByIdUseCase
    {
        Task<IEnumerable<ClientUserDto>> GetByIdAsync(int id);
    }
}
