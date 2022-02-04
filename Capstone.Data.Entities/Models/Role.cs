using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            SubRoles = new HashSet<SubRole>();
        }

        [Key]
        public int RoleId { get; set; }
        [StringLength(20)]
        public string RoleName { get; set; } = null!;

        [InverseProperty(nameof(SubRole.Role))]
        public virtual ICollection<SubRole> SubRoles { get; set; }
    }
}
