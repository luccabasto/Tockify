using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ToDo
{
    public interface ICreateToDoCase
    {
        Task<ToDoDto> CreateToDoAsync(CreateToDoCommand command);

    }
}
