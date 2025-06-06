using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tockify.Domain.Models
{
    public class AdmUserModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("admUserId")]
        public string Id { get;  private set; }

        public string Name { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }

        public AdmUserModel(string id, string name, string email) 
        { 
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name), "Nome não pode ser vazio.");
            Email = email?.Trim().ToLower() ?? throw new ArgumentNullException(nameof(email), "Email não pode ser vazio.");
        }

    }
}
