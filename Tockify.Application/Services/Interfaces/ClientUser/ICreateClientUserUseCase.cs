using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface ICreateClientUserUseCase
    {
        Task<ClientUserDto> CreateClientUser(CreateClientUserCommand command);
    }
}
