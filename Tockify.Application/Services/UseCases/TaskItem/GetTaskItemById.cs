using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.TaskItem;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.TaskItem
{
    public class GetTaskItemById : IGetTaskItemByIdCase
    {

        private readonly ITaskItemRepository _repository;
        private readonly IToDoListRepository _toDoRepository;
        private readonly IMapper _mapper;

        public GetTaskItemById(ITaskItemRepository repository, IMapper mapper, IToDoListRepository toDoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _toDoRepository = toDoRepository;
        }

        public async Task<TaskItemDto> GetTaskItemByIdAsync(string taskId, int userId)
        {
            var task = await _repository.GetTaskByIdAsync(taskId)
                        ?? throw new KeyNotFoundException($"TaskItem {taskId} não encontrado.");

            var todo = await _toDoRepository.GetByIdAsync(task.ToDoId)
                        ?? throw new KeyNotFoundException("ToDo não encontrado.");
            if (todo.CreatedByUserId != userId)
                throw new UnauthorizedAccessException("Acesso negado: ToDo não pertence ao usuário.");
            return _mapper.Map<TaskItemDto>(task);
        }
    }
}
