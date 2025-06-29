using Tockify.Domain.Enums;

namespace Tockify.Application.DTOs
{
    public class ToDoSummaryDto
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public ToDoStatus Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? TaskItemId { get; set; }
    }
}
