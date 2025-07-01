using Tockify.Domain.Enums;


namespace Tockify.Application.Services.Interfaces.ClientUser
{
    public interface IDeleteClientUserCase
    {
        Task DeleteClientUser(int id, UserProfile callerProfile);
    }
}
