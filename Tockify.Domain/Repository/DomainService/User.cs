using Tockify.Domain.Enums;
using Tockify.Domain.Models;

namespace Tockify.Domain.Repository.DomainService
{
    internal class User : UserModel
    {

        public User(string name, string email, object passwordHash, UserProfile profile)
        {
            Name = name;
            Email = email;
            this.passwordHash = passwordHash;
            Profile = profile;
        }
    }
}