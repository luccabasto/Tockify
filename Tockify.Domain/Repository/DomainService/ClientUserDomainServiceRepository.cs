
using Tockify.Domain.Enums;
using Tockify.Domain.Repository.Interface;
using BCrypt.Net;
using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.DomainService
{
    public class ClientUserDomainServiceRepository
    {
        private readonly IClientUserRepository _userRepository;

        public ClientUserDomainServiceRepository(IClientUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUserAsync(string name, string email, string password, UserProfile profile, Guid id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Nome não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentNullException(nameof(email), "Email inválido.");

            var existing = await _userRepository.GetUserByEmailAsync(email);
            if (existing != null)
                throw new InvalidOperationException("Email já está registrado.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new ArgumentNullException(nameof(password), "Senha deve ter pelo menos 6 caracteres.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password.Trim(), BCrypt.Net.BCrypt.GenerateSalt(12));
        }
    }
    
}
