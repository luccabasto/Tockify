/* using MongoDB.Driver;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Data;

namespace Tockify.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly MongoContext _context;

        public TaskItemRepository(MongoContext mongoContext)
        {
            _context = mongoContext;
        }

        // Retorna todas as sub‐tarefas (TaskItems) associadas a uma TaskList (taskListId)
        public async Task<List<TaskItemModel>> GetItemsByTaskIdAsync(Guid taskListId)
        {
            var filter = Builders<TaskItemModel>.Filter.Eq(x => x.ToDoListId, taskListId.ToString());
            return await _context.TaskItems.Find(filter).ToListAsync();
        }

        // Retorna uma sub‐tarefa pelo seu Id
        public async Task<TaskItemModel?> GetItemByIdAsync(Guid id)
        {
            var filter = Builders<TaskItemModel>.Filter.Eq(x => x.Id, id.ToString());
            return await _context.TaskItems.Find(filter).FirstOrDefaultAsync();
        }

        // Insere uma nova sub‐tarefa (TaskItem)
        public async Task<TaskItemModel> AddItemAsync(TaskItemModel item)
        {
            if (string.IsNullOrWhiteSpace(item.Id))
                item.Id = Guid.NewGuid().ToString();

            if (item.CreatedAt == default)
                item.CreatedAt = DateTime.UtcNow;

            item.IsCompleted = false;

            await _context.TaskItems.InsertOneAsync(item);
            return item;
        }

        // Atualiza uma sub‐tarefa existente
        public async Task<TaskItemModel> UpdateItemAsync(TaskItemModel item)
        {
            var filter = Builders<TaskItemModel>.Filter.Eq(x => x.Id, item.Id);
            await _context.TaskItems.ReplaceOneAsync(filter, item);
            return item;
        }

        // Exclui uma sub‐tarefa pelo Id
        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var filter = Builders<TaskItemModel>.Filter.Eq(x => x.Id, id.ToString());
            var result = await _context.TaskItems.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}

*/
