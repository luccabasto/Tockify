using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tockify.Domain.Enums;

namespace Tockify.Domain.Models
{
    public class ClientUserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public UserProfile? Profile { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
 
        public ClientUserModel(){}

        // Construtor adicional para inicialização com parâmetros  
        public ClientUserModel(string name, string email, string password, UserProfile profile)
        {
            Name = name;
            Email = email;
            Password = password;
            Profile = profile;
        }

        public ClientUserModel(string name, string email, string password, string gender, UserProfile profile)
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
            Profile = profile;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
