using AutoMapper;
using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ToDo;
using Tockify.Domain.Enums;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ToDo
{
    public class UpdateToDoCase : IUpdateToDoCase
    {
        private readonly IToDoListRepository _toDoRepo;
        private readonly IClientUserRepository _clientUserRepo;
        private readonly IMapper _mapper;
        public UpdateToDoCase(
            IToDoListRepository toDoRepo,
            IClientUserRepository clientUserRepo,
            IMapper mapper)
        {
            _toDoRepo = toDoRepo;
            _clientUserRepo = clientUserRepo;
            _mapper = mapper;
        }
        public async Task<ToDoDto> UpdateToDoAsync(UpdateToDoCommand command)
        {
            var existing = await _toDoRepo.GetByIdAsync(command.id) ?? throw new KeyNotFoundException("ToDo não encontrado.");

            existing.Title = command.Title ?? existing.Title;
            existing.Description = command.Description ?? existing.Description;
            existing.Flags = command.Flags ?? existing.Flags;
            existing.Status = command.Status ?? existing.Status;
            existing.DueDate = command.DueDate ?? existing.DueDate;
            existing.TaskItemId = command.TaskItemId ?? existing.TaskItemId;
            existing.UpdatedAt = DateTime.UtcNow;

            if (existing.CreatedByUserId != command.CreatedByUserId)
                throw new UnauthorizedAccessException("Usuário não autorizado a atualizar esta tarefa.");

            if(command.Flags != null && command.Flags.Count == 0)
                throw new ArgumentException("Ao menos uma flag é obrigatória.");

            if (command.DueDate.HasValue && command.DueDate.Value < existing.CreatedAt)
                throw new ArgumentException("DueDate não pode ser anterior à data de criação.");

            var before = existing.Status;
            var after = command.Status ?? existing.Status;

            _mapper.Map(command, existing);
            existing.Status = after;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _toDoRepo.UpdateAsync(existing) ?? throw new InvalidOperationException("Erro ao atualizar ToDo.");

            bool wasIncomplete = before == ToDoStatus.ToDo || before == ToDoStatus.InProgress;
            bool nowComplete = after == ToDoStatus.Concluded || after == ToDoStatus.Canceled;
            if (wasIncomplete && nowComplete)
                await _clientUserRepo.DecrementIncompleteCountAsync(command.CreatedByUserId);

            return _mapper.Map<ToDoDto>(updated);
        }
    }
}
