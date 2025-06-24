using System;
using System.Collections.Generic;
using System.Linq;
using Tockify.Application.Services.Interfaces.ToDo;
using Tockify.Domain.Enums;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Command.ToDo
{
    public class DeleteToDoCase : IDeleteToDoCase
    {
        private readonly IToDoListRepository _toDoRepo;
        private readonly IClientUserRepository _clientUserRepo;
        public DeleteToDoCase(IToDoListRepository toDoRepo, IClientUserRepository clientUserRepo)
        {
            _toDoRepo = toDoRepo;
            _clientUserRepo = clientUserRepo;
        }
        public async Task<bool> DeleteToDoAsync(string id, int userId)
        {
            var existing = await _toDoRepo.GetByIdAsync(id) ?? throw new KeyNotFoundException("ToDo não encontrado.");
            if (existing.CreatedByUserId != userId)
                throw new UnauthorizedAccessException("Usuário não autorizado a deletar esta tarefa.");

            if (existing.Status == ToDoStatus.ToDo || existing.Status == ToDoStatus.InProgress)
                await _clientUserRepo.DecrementIncompleteCountAsync(userId);

            return await _toDoRepo.DeleteAsync(id);
        }
    }
}
