using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("EmploymentHistory")]
    public partial class EmploymentHistory
    {
        [Key]
        public int EmploymentHistoryId { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; } = null!;
        [StringLength(100)]
        public string CompanyName { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime DateStarted { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateEnded { get; set; }
        [StringLength(50)]
        public string Position { get; set; } = null!;

        public virtual Holder National { get; set; } = null!;
    }
}
