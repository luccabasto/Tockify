using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItemModel>> GetItemsByTaskIdAsync(Guid taskListId);
        Task<TaskItemModel?> GetItemByIdAsync(Guid id);
        Task<TaskItemModel> AddItemAsync(TaskItemModel item);
        Task<TaskItemModel> UpdateItemAsync(TaskItemModel item);
        Task<bool> DeleteItemAsync(Guid id);
    }
}
