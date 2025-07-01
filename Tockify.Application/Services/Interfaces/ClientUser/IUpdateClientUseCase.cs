using Tockify.Application.Command.ClientUser;
using Tockify.Application.DTOs;
using Tockify.Domain.Enums;

namespace Tockify.Application.Services.Interfaces.ClientUser
{
     public interface IUpdateClientUseCase
    {
        Task<ClientUserDto> UpdateClientUser(UpdateClientUserCommand command, string? email);
    }
}
