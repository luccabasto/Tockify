using Tockify.Application.Command.TaskItem;
using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.TaskItem
{
    public interface ICreateTaskItemCase
    {
        Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemCommand command);
    }
}
