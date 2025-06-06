using Tockify.Application.DTOs;

namespace Tockify.Application.Services.UseCases.Interfaces
{
    public interface IGetAllClientUsersUseCase
    {
        Task<IEnumerable<ClientUserDto>> ExecuteAsync();
    }
}
