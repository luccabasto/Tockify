using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ToDo
{
    public interface IGetTaskItemsByTaskListUseCase
    {
        Task<IEnumerable<ToDoDto>> ExecuteAsync(Guid userId);
    }
}
