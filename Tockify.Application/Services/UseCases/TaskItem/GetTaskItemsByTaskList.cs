using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.UseCases.Interfaces;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.TaskItem
{
    public class GetTaskItemsByTaskListUseCase : IGetTaskItemsByTaskListUseCase
    {
        private readonly ITaskItemRepository _itemRepo;
        private readonly IMapper _mapper;

        public GetTaskItemsByTaskListUseCase(
            ITaskItemRepository itemRepo,
            IMapper mapper)
        {
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDto>> ExecuteAsync(Guid taskListId)
        {
            if (taskListId == Guid.Empty)
                throw new ArgumentException("TaskListId não pode ser vazio.", nameof(taskListId));

            var entities = await _itemRepo.GetItemsByTaskIdAsync(taskListId);
            var dtos = entities.Select(e => _mapper.Map<TaskItemDto>(e));
            return dtos;
        }

        Task<IEnumerable<ToDoDto>> IGetTaskItemsByTaskListUseCase.ExecuteAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
