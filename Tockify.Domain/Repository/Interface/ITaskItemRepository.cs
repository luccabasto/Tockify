using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItemModel>> GetItemsByTaskIdAsync(Guid taskId);
        Task<TaskItemModel?> GetItemByIdAsync(Guid itemId);
        Task AddItemAsync(TaskItemModel item);
        Task UpdateItemAsync(TaskItemModel item);
        Task DeleteItemAsync(Guid itemId);
        Task<bool> ItemExistsAsync(Guid itemId);
    }
}
