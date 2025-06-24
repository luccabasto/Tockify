using Tockify.Application.DTOs;

namespace Tockify.Application.Services.Interfaces.ToDo
{
    public interface IGetUserToDosCase
    {
        Task<List<ToDoDto>> ExecuteAsync(int userId);
    }
}
