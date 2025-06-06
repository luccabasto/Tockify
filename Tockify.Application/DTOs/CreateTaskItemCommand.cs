
namespace Tockify.Application.DTOs
{
    public class CreateTaskItemCommand
    {
        public Guid TaskListId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}
