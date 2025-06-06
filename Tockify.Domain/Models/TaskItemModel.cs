using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tockify.Domain.Models
{
    public class TaskItemModel
    {
        public int Id { get; set; }
        public double TaskId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public int? Status { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TaskItemModel(int id, string name, double taskId, DateTime dueDate)
        {
            Id = id;
            Name = name;
            TaskId = taskId;
            DueDate = dueDate;
            CreatedAt = DateTime.Now;
        }
    }
}
