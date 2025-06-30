using Tockify.Domain.Models;


namespace Tockify.Domain.Repository.Interface
{
    public interface ITaskItemRepository
    {
        Task<TaskItemModel> CreateTaskItemAsync(TaskItemModel task);
        Task<List<TaskItemModel>> GetByToDoAsync(string toDoId);
        Task<TaskItemModel?> GetTaskByIdAsync(string id);
        Task<TaskItemModel?> UpdateTaskAsync(TaskItemModel task);
        Task<bool> DeleteTaskAsync(string id);
    }
}
