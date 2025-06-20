using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tockify.Domain.Models
{
   public class CardModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("cardId")]
        public string Id { get; set; }

        public ClientUserModel UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public CardModel(string id, string name, ClientUserModel userId, DateTime dueDate)
        {
            Id = id;
            Name = name;
            UserId = userId;
            DueDate = dueDate;
            CreatedAt = DateTime.Now;
        
        }

        public CardModel()
        {
        }

        public CardModel(string id, string userId, string name, DateTime dueDate)
        {
            Id = id;
            Name = name;
            DueDate = dueDate;
        }
    }
}
