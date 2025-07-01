using System.ComponentModel.DataAnnotations;
using Tockify.Domain.Enums;

namespace Tockify.Application.Command.TaskItem
{
    public class UpdateTaskItemCommand
    {

        [Required]
        public string Id { get; set; } = null!;

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
        public TaskItemStatus? Status { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }
    }
}
