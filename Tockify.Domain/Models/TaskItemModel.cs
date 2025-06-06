using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tockify.Domain.Enums;

namespace Tockify.Domain.Models
{
    public class TaskItemModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("taskItemId")]
        public string Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusTask? Status { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? UpdatedAt { get; set; }

        protected TaskItemModel() { }
        public TaskItemModel(string id, string name, double taskId, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("O nome da tarefa não pode ser vazio.");
            Id = id;
            Name = name;
            DueDate = dueDate;
            CreatedAt = DateTime.Now;
            IsCompleted = false;
        }
    }
}
