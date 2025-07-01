using AutoMapper;
using Tockify.Application.Command.TaskItem;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.TaskItem;
using Tockify.Domain.Enums;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.TaskItem
{
    public class UpdateTaskItemCase : IUpdateTaskItemCase
    {

        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IMapper _mapper;

        public UpdateTaskItemCase(
            ITaskItemRepository taskItemRepository,
            IToDoListRepository toDoListRepository,
            IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _toDoListRepository = toDoListRepository;
            _mapper = mapper;
        }
        public async Task<TaskItemDto> UpdateTaskItemAsync(UpdateTaskItemCommand command)
        {

            var existing = await _taskItemRepository.GetTaskByIdAsync(command.Id)
                           ?? throw new KeyNotFoundException("TaskItem não encontrado.");

            var todo = await _toDoListRepository.GetByIdAsync(existing.ToDoId)
                       ?? throw new KeyNotFoundException("ToDo não encontrado.");
            if (todo.CreatedByUserId != command.CreatedByUserId)
                throw new UnauthorizedAccessException("Acesso negado: ToDo não pertence ao usuário.");

            if (command.DueDate.HasValue && command.DueDate < existing.CreatedAt)
                throw new ArgumentException("DueDate não pode ser anterior à data de criação.");

            var wasPending = existing.Status == TaskItemStatus.Pending;
            var willComplete = command.Status == TaskItemStatus.Completed;

            _mapper.Map(command, existing);

            if (willComplete && wasPending)
                existing.CompletedAt = DateTime.UtcNow;
            existing.Status = command.Status ?? existing.Status;

            var updated = await _taskItemRepository.UpdateTaskAsync(existing)
                           ?? throw new InvalidOperationException("Falha ao atualizar TaskItem.");

            return _mapper.Map<TaskItemDto>(updated);
        }
    }
}
