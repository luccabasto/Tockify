using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.TaskItem
{
    public interface ICreateTaskItemUseCase
    {
        Task<TaskItemDto> ExecuteAsync(CreateTaskItemCommand command);
    }
}
