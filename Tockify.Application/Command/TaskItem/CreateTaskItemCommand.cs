using System.ComponentModel.DataAnnotations;


namespace Tockify.Application.Command.TaskItem
{
    public class CreateTaskItemCommand
    {
        [Required, MinLength(1)]
        public string Title { get; set; } = null!;

        [Required, MinLength(1)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string ToDoId { get; set; } = null!;

        [Required]
        public int CreatedByUserId { get; set; }
        
        
       
    }
}
