using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ToDo;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.Implementations
{
    public class GetUserToDosCase : IGetUserToDosCase
    {
        private readonly IToDoListRepository _toDoRepo;
        private readonly IMapper _mapper;

        public GetUserToDosCase(
            IToDoListRepository taskListRepo,
            IMapper mapper)
        {
            _toDoRepo = taskListRepo;
            _mapper = mapper;
        }

        public async Task<List<ToDoDto>> ExecuteAsync(int userId)
        {
            var all = await _toDoRepo.GetByUserAsync(userId);
            return all.Select(x => _mapper.Map<ToDoDto>(x)).ToList();
        }
    }
}
