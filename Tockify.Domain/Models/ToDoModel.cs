using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tockify.Domain.Enums;

namespace Tockify.Domain.Models
{
   public class ToDoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("flags")]
        public List<string>? Flags { get; set; } = new List<string>();

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public ToDoStatus Status { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("dueDate")]
        public DateTime? DueDate { get; set; }

        [BsonElement("createdByUserId")]
        public int CreatedByUserId { get; set; }

        [BsonElement("taskItemId")]
        public int? TaskItemId { get; set; }
    }
}
