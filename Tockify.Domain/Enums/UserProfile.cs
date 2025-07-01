using System.ComponentModel;


namespace Tockify.Domain.Enums
{
    public enum UserProfile
    {

        [Description("ADMUser")]
        Admin = 0,

        [Description("ClientUser")]
        Client = 1,
    }
}
