using MongoDB.Driver;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Context;

namespace Tockify.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoListRepository
    {
        private readonly IMongoCollection<ToDoModel> _collection;

        public ToDoRepository(MongoContext context)
        {
            _collection = context.Database.GetCollection<ToDoModel>("todos");
        }

        public async Task<ToDoModel> InsertAsync(ToDoModel todo)
        {
            await _collection.InsertOneAsync(todo);
            return todo;
        }

        public async Task<List<ToDoModel>> GetByUserAsync(int userId)
        {
            var filter = Builders<ToDoModel>.Filter.Eq(t => t.CreatedByUserId, userId);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<ToDoModel?> GetByIdAsync(string id)
        {
            var filter = Builders<ToDoModel>.Filter.Eq(t => t.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ToDoModel?> UpdateAsync(ToDoModel todo)
        {
            var filter = Builders<ToDoModel>.Filter.Eq(t => t.Id, todo.Id);
            var result = await _collection.ReplaceOneAsync(filter, todo);
            return result.IsAcknowledged && result.ModifiedCount > 0
                ? todo
                : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<ToDoModel>.Filter.Eq(t => t.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
