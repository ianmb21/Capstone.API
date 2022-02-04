using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("SubRole")]
    public partial class SubRole
    {
        public SubRole()
        {
            SubRoleMatrices = new HashSet<SubRoleMatrix>();
            Users = new HashSet<User>();
        }

        [Key]
        public int SubRoleId { get; set; }
        [StringLength(50)]
        public string SubRoleName { get; set; } = null!;
        public int RoleId { get; set; }
        [StringLength(100)]
        public string? Industry { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("SubRoles")]
        public virtual Role Role { get; set; } = null!;
        [InverseProperty(nameof(SubRoleMatrix.SubRole))]
        public virtual ICollection<SubRoleMatrix> SubRoleMatrices { get; set; }
        [InverseProperty(nameof(User.SubRole))]
        public virtual ICollection<User> Users { get; set; }
    }
}
