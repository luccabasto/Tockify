namespace Tockify.Application.Command.ToDo
{
    public class CreateToDoCommand
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}
