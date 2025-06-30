using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tockify.Domain.Enums;

namespace Tockify.Domain.Models
{
    public class TaskItemModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("description")]
        public string? Description { get; set; } = null!;

        [BsonElement("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("dueDate")]
        public DateTime? DueDate { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public TaskItemStatus? Status { get; set; }

        [BsonElement("isCompleted")]
        public bool? IsCompleted { get; set; }

        [BsonElement("updateAt")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("completedAt")]
        public DateTime? CompletedAt { get; set; }

        [BsonElement("tDoId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ToDoId { get; set; } = null!;

        [BsonElement("createdByUserId")]
        [BsonRepresentation(BsonType.Int32)]
        public int CreatedByUserId { get; set; }

        public TaskItemModel()
        {
        }
    }
}
