using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.UseCases.Interfaces;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.Implementations
{
    public class GetToDoListsByUserUseCase : IGetToDoByUserUseCase
    {
        private readonly IToDoListRepository _taskListRepo;
        private readonly IMapper _mapper;

        public GetToDoListsByUserUseCase(
            IToDoListRepository taskListRepo,
            IMapper mapper)
        {
            _taskListRepo = taskListRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoDto>> ExecuteAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId não pode ser vazio.", nameof(userId));

            var entities = await _taskListRepo.GetTasksByUserIdAsync(userId);
            var dtos = entities.Select(e => _mapper.Map<ToDoDto>(e));
            return dtos;
        }
    }
}
