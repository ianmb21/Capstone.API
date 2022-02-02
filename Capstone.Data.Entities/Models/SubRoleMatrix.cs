using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("SubRoleMatrix")]
    public partial class SubRoleMatrix
    {
        [Key]
        public int SubRoleMatrixId { get; set; }
        public int? RecordTypeId { get; set; }
        public int SubRoleId { get; set; }

        [ForeignKey(nameof(SubRoleId))]
        [InverseProperty("SubRoleMatrices")]
        public virtual SubRole SubRole { get; set; } = null!;
    }
}
