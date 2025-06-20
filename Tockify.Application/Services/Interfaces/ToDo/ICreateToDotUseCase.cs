using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ToDo
{
    public interface ICreateToDotUseCase
    {
        Task<ToDoDto> ExecuteAsync(CreateToDoCommand command);

    }
}
