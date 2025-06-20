using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tockify.Application.Services.Interfaces.ClientUser;
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

        public async Task DeleteClientUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException("O ID deve ser maior que zero.");

            var existing = await _repository.GetUserByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"Usuário com ID {id} não encontrado.");

            await _repository.DeleteClientUserByIdAsync(id);
        }
    }
}
