using AutoMapper;
using System.Text.RegularExpressions;
using Tockify.Application.Command.ClientUser;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ClientUser
{
    public class CreateClientUserUseCase : ICreateClientUserCase
    {
        private readonly IClientUserRepository _repository;
        private readonly IMapper _mapper;
        private static readonly Regex EmailRegex =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public CreateClientUserUseCase(IClientUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClientUserDto> CreateClientUser(CreateClientUserCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(command.Email) || !EmailRegex.IsMatch(command.Email))
                throw new ArgumentException("A valid email is required.");
            if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.");
            {
                if (await _repository.ClientUserExistsAsync(command.Email))
                    throw new InvalidOperationException("Email already in use.");

                var model = new ClientUserModel(
                    command.Name,
                    command.Email,
                    command.Password,
                    command.Gender,
                    command.Profile
                );

                var created = await _repository.RegisterUserAsync(model, command.Email, command.Password);
                return _mapper.Map<ClientUserDto>(created);
            }

            throw new ArgumentException("Invalid profile.");
        }
    }
}

