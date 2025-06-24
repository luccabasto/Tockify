using System.ComponentModel.DataAnnotations;
using Tockify.Domain.Enums;

namespace Tockify.Application.Command.ToDo
{
    public class CreateToDoCommand
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int CreatedByUserId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "Pelo menos uma flag é obrigatória.")]
        public List<string> Flags { get; set; } = new();

        public ToDoStatus? Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public int? TaskItemId { get; set; }
    }
}
