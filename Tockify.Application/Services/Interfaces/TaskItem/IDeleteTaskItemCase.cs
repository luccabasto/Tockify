


namespace Tockify.Application.Services.Interfaces.TaskItem
{
    public interface IDeleteTaskItemCase
    {
        Task<bool> DeleteTaskItemAsync(string id, int userId);
    }
}
