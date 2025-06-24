using MongoDB.Bson;
using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ToDo
{
    public interface IUpdateToDoCase
    {
        Task<ToDoDto> UpdateToDoAsync(UpdateToDoCommand command);
    }
}
