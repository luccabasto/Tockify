using Tockify.Domain.Enums;

namespace Tockify.Application.Command.ClientUser
{
    public class CreateClientUserCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Gender { get; set; }
        public UserProfile Profile { get; set; } = UserProfile.Client;
        public int IncompleteToDosCount { get; set; }
    }
}
