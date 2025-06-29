namespace Tockify.Application.Services.Interfaces.ToDo
{
    public interface IDeleteToDoCase
    {
        Task<bool> DeleteToDoAsync(string id, int userId);
    }
}
