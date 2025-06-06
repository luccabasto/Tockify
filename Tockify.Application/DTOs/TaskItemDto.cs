namespace Tockify.Application.DTOs
{
    public class TaskItemDto
    {
        public Guid Id { get; set; }
        public Guid TaskListId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
