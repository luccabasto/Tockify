using AutoMapper;
using Tockify.Application.Command.ClientUser;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ClientUser
{
    public class UpdateClientUserUseCase : IUpdateClientUseCase
    {
        private readonly IClientUserRepository _repository;
        private readonly IMapper _mapper;

        public UpdateClientUserUseCase(IClientUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClientUserDto> UpdateClientUser(UpdateClientUserCommand command)
        {
            if (command.Id <= 0)
                throw new ArgumentException("O ID deve ser maior que zero.");
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Name é obrigatório.");
            if (string.IsNullOrWhiteSpace(command.Email))
                throw new ArgumentException("Email é obrigatório.");

            var existing = await _repository.GetUserByIdAsync(command.Id);
            if (existing == null)
                throw new InvalidOperationException($"Usuário com ID {command.Id} não encontrado.");

            // Atualiza somente os campos permitidos
            existing.Name = command.Name;
            existing.Email = command.Email;
            existing.Gender = command.Gender;

            var updated = await _repository.UpdateClientUserByIdAsync(existing, existing.Email, existing.Password);
            return _mapper.Map<ClientUserDto>(updated);
        }
    }
}
