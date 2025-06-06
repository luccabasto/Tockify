using Tockify.Application.DTOs;

namespace Tockify.Application.Services.UseCases.Interfaces
{
    public interface IGetToDoByUserUseCase
    {
        Task<IEnumerable<ToDoDto>> ExecuteAsync(Guid userId);
    }
}
