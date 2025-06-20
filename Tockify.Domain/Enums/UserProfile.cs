using System.ComponentModel;


namespace Tockify.Domain.Enums
{
    public enum UserProfile
    {
        [Description("User ID")]
        Id,

        [Description("ADM User")]
        Admin,

        [Description("Client User")]
        Client,
    }
}
