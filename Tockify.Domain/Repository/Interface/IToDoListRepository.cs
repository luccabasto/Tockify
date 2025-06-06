using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IToDoListRepository
    {
        Task<List<CardModel>> GetTasksByUserIdAsync(Guid userId);
        Task<CardModel?> GetTaskByIdAsync(Guid id);
        Task<CardModel> AddTaskAsync(CardModel taskList);
        Task<CardModel> UpdateTaskAsync(CardModel taskList);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
