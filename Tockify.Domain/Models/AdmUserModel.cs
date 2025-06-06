using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tockify.Domain.Models
{
    public class AdmUserModel
    {
        public int Id { get;  private set; }
        public string Name { get; private set; }
        public string? Email { get; private set; }
        public string Password { get; private set; }

    }
}
