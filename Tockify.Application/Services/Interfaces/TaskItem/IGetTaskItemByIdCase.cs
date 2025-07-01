using Tockify.Application.DTOs;


namespace Tockify.Application.Services.Interfaces.TaskItem
{
    public interface IGetTaskItemByIdCase
    {
        Task<TaskItemDto>  GetTaskItemByIdAsync(string taskId, int userId);
    }
}
