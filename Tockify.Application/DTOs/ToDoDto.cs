using Tockify.Domain.Enums;

namespace Tockify.Application.DTOs
{
    public class ToDoDto
    {
        public string Id { get; set; } = null;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<string> Flags { get; set; } = new();
        public ToDoStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int? TaskItemId { get; set; }

        public int TotalTasksCount { get; set; }
        public int PendingTasksCount { get; set; }
        public int CompletedTasksCount { get; set; }
        public List<TaskItemSummaryDto> CompletedTasks { get; set; } = new();
    }
}
