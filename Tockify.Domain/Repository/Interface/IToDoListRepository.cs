using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<CardModel>> GetTasksByUserIdAsync(Guid userId);
        Task<CardModel?> GetTaskByIdAsync(Guid taskId);
        Task AddTaskAsync(CardModel task);
        Task UpdateTaskAsync(CardModel task);
        Task DeleteTaskAsync(Guid taskId);
        Task<bool> TaskExistsAsync(Guid taskId);
    }
}
