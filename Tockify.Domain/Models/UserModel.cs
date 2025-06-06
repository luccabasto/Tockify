using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tockify.Domain.Enums;

namespace Tockify.Domain.Models
{
   public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Gender { get; set; }
        public DateTime? CreatedAt { get; set; }
        
        public UserProfile Profile { get; set; } = UserProfile.Client;
        public bool IsActive { get; set; } = true;

        public List<CardModel>? Tasks { get; set; } = new List<CardModel>();

        public UserModel(string name, string email, string password, UserProfile userProfile)
        {
            Id = Guid.NewGuid();
            Name = name?.Trim() ?? throw new ArgumentNullException(nameof(name), "Nome não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email não pode ser vazio.");
            Email = email.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Senha não pode ser vazia.");

            if (password.Length < 6)
                Password = password;

            CreatedAt = DateTime.Now;
            Profile = userProfile;
        }
    }
}
