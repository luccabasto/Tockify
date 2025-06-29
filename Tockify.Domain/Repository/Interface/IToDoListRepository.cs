using MongoDB.Bson;
using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.Interface
{
    public interface IToDoListRepository
    {
        Task<List<ToDoModel>> GetByUserAsync(int userId); //Buscar todo por user
        Task<ToDoModel> InsertAsync(ToDoModel todo);
        Task<ToDoModel?> GetByIdAsync(string id); //Buscar todo pelo id
        Task<ToDoModel?> UpdateAsync(ToDoModel todo);
        Task<bool> DeleteAsync(string id);
    }
}
