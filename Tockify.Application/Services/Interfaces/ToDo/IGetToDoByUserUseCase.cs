using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ToDo
{
    public interface IGetToDoByUserUseCase
    {
        Task<IEnumerable<ToDoDto>> ExecuteAsync(Guid userId);
    }
}
