using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Application.Services.UseCases.Interfaces;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ClientUser
{
    public class CreateClientUser : ICreateClientUserUseCase
    {
        private readonly IClientUserRepository _clientRepo;
        private readonly IMapper _mapper;

        public CreateClientUser
        (
                IClientUserRepository clientRepo,
                IMapper mapper
        )
        {
            _clientRepo = clientRepo;
            _mapper = mapper;

        }

        public async Task<ClientUserDto> ExecuteAsync(CreateClientUserCommand command)
        {
            // Validar dados mínimos
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Nome é obrigatório.", nameof(command.Name));
            if (string.IsNullOrWhiteSpace(command.Email))
                throw new ArgumentException("Email é obrigatório.", nameof(command.Email));
            if (string.IsNullOrWhiteSpace(command.Password))
                throw new ArgumentException("Password é obrigatório.", nameof(command.Password));

            // Verificar se usuário já existe
            var exists = await _clientRepo.ClientUserExistsAsync(command.Email); // Verificando se já existe usuário com o mesmo email
            if (exists)
                throw new InvalidOperationException($"Já existe usuário com email = {command.Email}");

            // Criar a entidade de domínio
            var entity = new ClientUserModel
            {
                Id = Guid.NewGuid(),
                Name = command.Name.Trim(),
                Email = command.Email.Trim().ToLower(),
                Password = command.Password,
                Gender = command.Gender,
                Profile = UserProfile.Client,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            //  Persistir via repositório (Mongo)
            var created = await _clientRepo.RegisterUserAsync(entity, entity.Email, entity.Password);

            //  Mapear para DTO de saída
            var dto = _mapper.Map<ClientUserDto>(created);
            return dto;
        }
    }
}
