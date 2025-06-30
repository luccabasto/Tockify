using AutoMapper;
using Tockify.Application.Command.TaskItem;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.TaskItem;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.TaskItem
{
    public class CreateTaskItemUseCase : ICreateTaskItemCase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IMapper _mapper;

        public CreateTaskItemUseCase(
            ITaskItemRepository taskRepo,
            IToDoListRepository todoRepo,
            IMapper mapper)
        {
            _taskItemRepository = taskRepo;
            _toDoListRepository = todoRepo;
            _mapper = mapper;
        }

       public async Task<TaskItemDto> CreateTaskItemAsync(CreateTaskItemCommand command)
        {

            if(string.IsNullOrWhiteSpace(command.Title))
                throw new ArgumentException("Título é obrigatório", nameof(command.Title));
            if (string.IsNullOrWhiteSpace(command.Description))
                throw new ArgumentException("Descrição é obrigatória", nameof(command.Description));
            if (command.DueDate < DateTime.UtcNow)
                throw new ArgumentException("A data de vencimento não pode ser no passado", nameof(command.DueDate));

            var todo = await _toDoListRepository.GetByIdAsync(command.ToDoId) ?? throw new KeyNotFoundException("ToDo não encontrado.");

            if (todo.CreatedByUserId != command.CreatedByUserId) throw new UnauthorizedAccessException("Acesso negado: ToDo não pertence ao usuário.");

            var model = _mapper.Map<TaskItemModel>(command);
            model.CreatedAt = DateTime.UtcNow;
            model.Status = TaskItemStatus.Pending;
            model.CompletedAt = null;

            var createdTask = await _taskItemRepository.CreateTaskItemAsync(model);
            return _mapper.Map<TaskItemDto>(createdTask);
        }
    }
}
