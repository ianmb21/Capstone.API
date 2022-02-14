using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("User")]
    public partial class User22
    {
        [Key]
        public int UserId { get; set; }
        public int SubRoleId { get; set; }
        [StringLength(50)]
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public int? HolderId { get; set; }

        [ForeignKey(nameof(SubRoleId))]
        [InverseProperty("Users")]
        public virtual SubRole SubRole { get; set; } = null!;
    }
}
