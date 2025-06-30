using Tockify.Application.Services.Interfaces.TaskItem;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.TaskItem
{
    public class DeleteTaskItemCase : IDeleteTaskItemCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IToDoListRepository _toDoListRepository;

        public DeleteTaskItemCase(
            ITaskItemRepository taskItemRepository,
            IToDoListRepository toDoListRepository)
        {
            _taskItemRepository = taskItemRepository;
            _toDoListRepository = toDoListRepository;
        }

        public async Task<bool> DeleteTaskItemAsync(string id, int userId)
        {
            var existing = await _taskItemRepository.GetTaskByIdAsync(id)
                ?? throw new KeyNotFoundException("TaskItem não encontrado.");

            var todo = await _toDoListRepository.GetByIdAsync(existing.ToDoId)
                ?? throw new KeyNotFoundException("ToDo não encontrado.");
            if (todo.CreatedByUserId != userId)
                throw new UnauthorizedAccessException("Acesso negado: ToDo não pertence ao usuário.");
            return await _taskItemRepository.DeleteTaskAsync(id);
        }
    }
}
