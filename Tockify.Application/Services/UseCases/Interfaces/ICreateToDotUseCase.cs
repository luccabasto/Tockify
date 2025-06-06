using Tockify.Application.DTOs;

namespace Tockify.Application.Services.UseCases.Interfaces
{
    public interface ICreateToDotUseCase
    {
        Task<ToDoDto> ExecuteAsync(CreateTaskItemCommand command);
        Task ExecuteAsync(CreateToDoCommand command);
    }
}
