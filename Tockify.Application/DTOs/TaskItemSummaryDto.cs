

namespace Tockify.Application.DTOs
{
    public class TaskItemSummaryDto
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
