using Tockify.Application.DTOs;

namespace Tockify.Application.Services.UseCases.Interfaces
{
    public interface ICreateTaskItemUseCase
    {
        Task<TaskItemDto> ExecuteAsync(CreateTaskItemCommand command);
    }
}
