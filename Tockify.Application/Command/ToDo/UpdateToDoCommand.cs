using System.ComponentModel.DataAnnotations;
using Tockify.Domain.Enums;

namespace Tockify.Application.Command.ToDo
{
    public class UpdateToDoCommand
    {
        [Required]
        public string id { get; set; } = null;

        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Flags { get; set; }
        public ToDoStatus? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public int? TaskItemId { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }


    }
}
