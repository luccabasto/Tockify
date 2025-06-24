using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ClientUser
{
    public class GetClientUserByIdCase : IGetClientUserByIdCase
    {
        private readonly IClientUserRepository _repository;
        private readonly IToDoListRepository _toDoRepository;
        private readonly IMapper _mapper;

        public GetClientUserByIdCase(IClientUserRepository repository, IMapper mapper, IToDoListRepository toDoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _toDoRepository = toDoRepository;
        }

        public async Task<ClientUserDto> GetUserByIdAsync(int userId)
        {
            var user = await _repository.GetUserByIdAsync(userId)
                        ?? throw new KeyNotFoundException($"ClientUser {userId} não encontrado.");

            var dto = _mapper.Map<ClientUserDto>(user);
            var todos = await _toDoRepository.GetByUserAsync(userId);

            dto.ToDos = todos.Select(t => new ToDoSummaryDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                UpdatedAt = t.UpdatedAt,
                TaskItemId = t.TaskItemId
            }).ToList();

            return dto;
        }
    }
}
