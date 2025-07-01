using MongoDB.Driver;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Context;

namespace Tockify.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly IMongoCollection<TaskItemModel> _taskCollection;

        public TaskItemRepository(MongoContext database)
        {
            _taskCollection = database.Database.GetCollection<TaskItemModel>("TaskItems");
        }

        public async Task<TaskItemModel> CreateTaskItemAsync(TaskItemModel task)
        {
            await _taskCollection.InsertOneAsync(task);
            return task;
        }

        public async Task<List<TaskItemModel>> GetByToDoAsync(string toDoId)
        {
            var filter = Builders<TaskItemModel>.Filter.Eq(t => t.ToDoId, toDoId);
            return await _taskCollection.Find(filter).ToListAsync();
        }

        public async Task<TaskItemModel?> GetTaskByIdAsync(string id)
        {
            var filter = Builders<TaskItemModel>.Filter.Eq(t => t.Id, id);
            return await _taskCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<TaskItemModel?> UpdateTaskAsync(TaskItemModel task)
        {
            var filter = Builders<TaskItemModel>.Filter.Eq(t => t.Id, task.Id);
            var update = Builders<TaskItemModel>.Update
                .Set(t => t.Title, task.Title)
                .Set(t => t.Description, task.Description)
                .Set(t => t.DueDate, task.DueDate)
                .Set(t => t.Status, task.Status)
                .Set(t => t.CompletedAt, task.CompletedAt);
            var result = await _taskCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0 ? task : null;
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            var filter = Builders<TaskItemModel>.Filter.Eq(t => t.Id, id);
            var result = await _taskCollection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}