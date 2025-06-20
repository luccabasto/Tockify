using Tockify.Application.Command.ClientUser;
using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface ICreateClientUserCase
    {
        Task<ClientUserDto> CreateClientUser(CreateClientUserCommand command);

    }
}
