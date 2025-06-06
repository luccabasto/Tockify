using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tockify.Domain.Models
{
   public class CardModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public CardModel(int id, string name, Guid userId, DateTime dueDate)
        {
            Id = id;
            Name = name;
            UserId = userId;
            DueDate = dueDate;
            CreatedAt = DateTime.Now;
        
        }
    }
}
