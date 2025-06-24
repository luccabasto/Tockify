using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Domain.Enums;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ClientUser
{
    public class GetAllClientUsersCase : IGetAllClientUsersCase
    {
        private readonly IClientUserRepository _clientRepo;
        private readonly IToDoListRepository _todoRepo;
        private readonly IMapper _mapper;

        public GetAllClientUsersCase(IClientUserRepository clientRepo, IMapper mapper, IToDoListRepository todoRepo)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
            _todoRepo = todoRepo;
        }

        public async Task<List<ClientUserDto>> GetAllClient(UserProfile? profile = null)
        {
            var users = profile.HasValue
                ? await _clientRepo.GetAllClientUsersAsync(profile.Value)
                : await _clientRepo.GetAllClientUsersAsync(profile.Value);

            var dtos = new List<ClientUserDto>();

            foreach (var u in users)
            {
                var dto = _mapper.Map<ClientUserDto>(u);

                var todos = await _todoRepo.GetByUserAsync(u.Id);

                dto.ToDos = todos.Select(t => new ToDoSummaryDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    UpdatedAt = t.UpdatedAt,
                    TaskItemId = t.TaskItemId
                }).ToList();

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}