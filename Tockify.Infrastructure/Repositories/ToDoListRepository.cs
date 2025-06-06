using MongoDB.Driver;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Data;

namespace Tockify.Infrastructure.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly MongoContext _context;

        public ToDoListRepository(MongoContext mongoContext)
        {
            _context = mongoContext;
        }

        // Retorna todas as listas de tarefas (cards) de um dado usuário (UserId)
        public async Task<List<CardModel>> GetTasksByUserIdAsync(Guid userId)
        {
            var filter = Builders<CardModel>.Filter.Eq(x => x.UserId.Id, userId);
            return await _context.TaskLists.Find(filter).ToListAsync();
        }

        // Retorna uma lista de tarefas (card) pelo seu Id
        public async Task<CardModel?> GetTaskByIdAsync(Guid id)
        {
            var filter = Builders<CardModel>.Filter.Eq(x => x.Id, id.ToString());
            return await _context.TaskLists.Find(filter).FirstOrDefaultAsync();
        }

        // Insere uma nova lista de tarefas (card)
        public async Task<CardModel> AddTaskAsync(CardModel taskList)
        {
            if (string.IsNullOrWhiteSpace(taskList.Id))
                taskList.Id = Guid.NewGuid().ToString();

            // Garante CreatedAt antes de inserir se não tiver sido atribuído
            if (taskList.CreatedAt == default)
                taskList.CreatedAt = DateTime.UtcNow;

            await _context.TaskLists.InsertOneAsync(taskList);
            return taskList;
        }

        // Atualiza uma lista de tarefas existente
        public async Task<CardModel> UpdateTaskAsync(CardModel taskList)
        {
            var filter = Builders<CardModel>.Filter.Eq(x => x.Id, taskList.Id);
            // Substitui todo o documento pelo objeto atualizado
            await _context.TaskLists.ReplaceOneAsync(filter, taskList);
            return taskList;
        }

        // Exclui uma lista de tarefas pelo Id
        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var filter = Builders<CardModel>.Filter.Eq(x => x.Id, id.ToString());
            var result = await _context.TaskLists.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
