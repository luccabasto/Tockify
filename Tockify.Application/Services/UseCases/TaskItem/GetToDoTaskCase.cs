using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.TaskItem;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.TaskItem
{
    public class GetToDoTaskCase : IGetToDoTasksCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IMapper _mapper;

        public GetToDoTaskCase(
            ITaskItemRepository taskItemRepository,
            IToDoListRepository toDoListRepository,
            IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _toDoListRepository = toDoListRepository;
            _mapper = mapper;
        }

        public async Task<List<TaskItemDto>> GetToDoTasksAsync(string toDoId, int userId)
        {
            var todo = await _toDoListRepository.GetByIdAsync(toDoId) ?? throw new KeyNotFoundException("ToDo não encontrado.");

            if (todo.CreatedByUserId != userId)
                throw new UnauthorizedAccessException("Acesso negado: ToDo não pertence ao usuário.");

            var tasks = await _taskItemRepository.GetByToDoAsync(toDoId);

            return tasks.Select(t => _mapper.Map<TaskItemDto>(t)).ToList();
        }
    }
}
