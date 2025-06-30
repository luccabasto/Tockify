

using Tockify.Domain.Enums;

namespace Tockify.Application.DTOs
{
    public class TaskItemDto
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime DueDate { get; set; }
        public TaskItemStatus Status { get; set; }
        public string ToDoId { get; set; } = null!;
        public int CreatedByUserId { get; set; }
    }
}
