using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface IGetAllClientUsersUseCase
    {
        Task<IEnumerable<ClientUserDto>> GetAllClient();
    }
}
