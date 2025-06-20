/* using AutoMapper;
using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;
using Tockify.Application.Services.Interfaces.ToDo;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;

namespace Tockify.Application.Services.UseCases.ToDo
{
    public class CreateToDoList : ICreateToDotUseCase
    {
        private readonly IToDoListRepository _taskListRepo;
        private readonly IMapper _mapper;

        public CreateToDoList(
            IToDoListRepository taskListRepo,
            IMapper mapper)
        {
            _taskListRepo = taskListRepo;
            _mapper = mapper;
        }

        public async Task<ToDoDto> ExecuteAsync(CreateToDoCommand command)
        {
            if (command.UserId == Guid.Empty)
                throw new ArgumentException("UserId não pode ser vazio.", nameof(command.UserId));
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Name do To-Do é obrigatório.", nameof(command.Name));

            // Montar entidade
            var entity = new CardModel(
                Guid.NewGuid().ToString(), // string com Guid
                command.Name.Trim(),
                new ClientUserModel { Id = command.UserId },
                DateTime.UtcNow
            )
            {
                Description = command.Description.Trim(),
                DueDate = command.DueDate
            };

            await _taskListRepo.AddTaskAsync(entity);

            // Mapear para DTO
            var dto = _mapper.Map<ToDoDto>(entity);
            return dto;
        }

    }
}
*/
