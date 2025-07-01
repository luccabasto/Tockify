using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Domain.Enums;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ClientUser
{
    public class DeleteClientUserUseCase : IDeleteClientUserCase
    {
        private readonly IClientUserRepository _repository;

        public DeleteClientUserUseCase(IClientUserRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteClientUser(int id, UserProfile callerProfile)
        {
            if (callerProfile != UserProfile.Admin)
                throw new UnauthorizedAccessException("Apenas administradores podem excluir usuários.");

            if (id <= 0)
                throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));

            var existing = await _repository.GetUserByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"Usuário com ID {id} não encontrado.");

            await _repository.DeleteClientUserByIdAsync(id);
        }
    }
}
