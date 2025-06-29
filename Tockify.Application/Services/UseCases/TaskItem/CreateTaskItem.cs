using AutoMapper;
using Tockify.Application.Command.TaskItem;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.TaskItem;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.TaskItem
{
    public class CreateTaskItemUseCase : ICreateTaskItemUseCase
    {
        private readonly ITaskItemRepository _itemRepo;
        private readonly IMapper _mapper;

        public CreateTaskItemUseCase(
            ITaskItemRepository itemRepo,
            IMapper mapper)
        {
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        public async Task<TaskItemDto> ExecuteAsync(CreateTaskItemCommand command)
        {

            if (command.TaskListId == Guid.Empty)
                throw new ArgumentException("ToDoListId não pode ser vazio.", nameof(command.TaskListId));
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Name da TaskItem é obrigatório.", nameof(command.Name));

            // Cria entidade
            var entity = new TaskItemModel
            {
                Id = Guid.NewGuid().ToString(),
                ToDoListId = command.TaskListId.ToString(),
                Title = command.Name.Trim(),
                Description = command.Description.Trim(),
                CreatedAt = DateTime.UtcNow,
                DueDate = command.DueDate,
                IsCompleted = false
            };

            await _itemRepo.AddItemAsync(entity);

            var dto = _mapper.Map<TaskItemDto>(entity);
            return dto;
        }
    }
}
