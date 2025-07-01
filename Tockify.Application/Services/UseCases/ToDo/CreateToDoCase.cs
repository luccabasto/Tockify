using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ToDo;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ToDo
{
    public class CreateToDoCase : ICreateToDoCase
    {
        private readonly IToDoListRepository _toDoRepo;
        private readonly IClientUserRepository _clientUserRepo;
        private readonly IMapper _mapper;

        public CreateToDoCase(
            IToDoListRepository toDoRepo,
            IClientUserRepository clientUserRepo,
            IMapper mapper)
        {
            _toDoRepo = toDoRepo;
            _clientUserRepo = clientUserRepo;
            _mapper = mapper;
        }

        public async Task<ToDoDto> CreateToDoAsync(CreateToDoCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Title))
                throw new ArgumentException("Título não pode ser vazio", nameof(command.Title));

            if (string.IsNullOrWhiteSpace(command.Description))
                throw new ArgumentException("Descrição não pode ser vazia", nameof(command.Description));

            if (command.Flags is null || command.Flags.Count == 0)
                throw new ArgumentException("Ao menos uma flag é obrigatória.");

            // Regras
            var user = await _clientUserRepo.GetUserByIdAsync(command.CreatedByUserId) ??
                throw new ArgumentException("Usuário não encontrado.", nameof(command.CreatedByUserId));

            var model = _mapper.Map<ToDoModel>(command);
            model.Status = command.Status ?? ToDoStatus.ToDo;
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = null;

            var createdToDo = await _toDoRepo.InsertAsync(model);

            if (createdToDo.Status == ToDoStatus.ToDo || createdToDo.Status == ToDoStatus.InProgress)
                await _clientUserRepo.IncrementIncompleteCountAsync(createdToDo.CreatedByUserId);

            return _mapper.Map<ToDoDto>(createdToDo);
        }
    }
}

