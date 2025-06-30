using Tockify.Application.Command.TaskItem;
using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.TaskItem
{
    public interface IUpdateTaskItemCase
    {
        Task<TaskItemDto> UpdateTaskItemAsync(UpdateTaskItemCommand command);
    }
}
