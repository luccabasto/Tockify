using Tockify.Application.DTOs;

namespace Tockify.Application.Services.UseCases.Interfaces
{
    public interface ICreateClientUserUseCase
    {
        Task<ClientUserDto> ExecuteAsync(CreateClientUserCommand command);
    }
}
