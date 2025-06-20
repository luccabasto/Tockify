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
        private readonly IMapper _mapper;

        public GetAllClientUsersCase(IClientUserRepository clientRepo, IMapper mapper)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientUserDto>> GetAllClient()
        {
            // Busca todos com perfil Client
            var entities = await _clientRepo.GetAllClientUsersAsync(UserProfile.Client);
            return entities
                   .Select(e => _mapper.Map<ClientUserDto>(e));
        }
    }
}