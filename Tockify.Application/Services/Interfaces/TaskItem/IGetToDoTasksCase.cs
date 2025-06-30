using Tockify.Application.DTOs;


namespace Tockify.Application.Services.Interfaces.TaskItem
{
    public interface IGetToDoTasksCase
    {
        Task<List<TaskItemDto>> GetToDoTasksAsync(string toDoId, int userId);
    }
}
