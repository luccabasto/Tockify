using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ClientUser
{
    public class GetClientUserByIdCase : IGetClientUserByIdCase
    {
        private readonly IClientUserRepository _repository;
        private readonly IMapper _mapper;

        public GetClientUserByIdCase(IClientUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientUserDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("O ID deve ser maior que zero.");

            var entity = await _repository.GetUserByIdAsync(id);
            if (entity == null)
                return Enumerable.Empty<ClientUserDto>();

            return new[] { _mapper.Map<ClientUserDto>(entity) };
        }
    }
}
