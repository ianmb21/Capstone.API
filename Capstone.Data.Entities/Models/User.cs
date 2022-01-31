using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
