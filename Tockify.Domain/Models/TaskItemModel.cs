using Tockify.Domain.Enums;

namespace Tockify.Domain.Models
{
    public class TaskItemModel
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusTask? Status { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? ToDoListId { get; set; }

        public TaskItemModel()
        {
        }
    }
}
