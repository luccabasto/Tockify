using AutoMapper;

using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Domain.Enums;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ClientUser
{
    public class GetAllClientUsers(
        IClientUserRepository clientRepo,
        IMapper mapper) : IGetAllClientUsersUseCase
    {
        private readonly IClientUserRepository _clientRepo = clientRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ClientUserDto>> ExecuteAsync()
        {
            // Buscando todos os perfils com o Profile = Client
            var entities = await _clientRepo.GetAllClientUsers(UserProfile.Client);
            // Mapeia cada entidade para DTO
            var dtos = entities.Select(e => _mapper.Map<ClientUserDto>(e));
            return dtos;
        }
    }
}
