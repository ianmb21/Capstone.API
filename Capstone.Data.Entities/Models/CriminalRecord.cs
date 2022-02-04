using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("CriminalRecord")]
    public partial class CriminalRecord
    {
        [Key]
        public int CriminalRecordId { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; } = null!;
        [StringLength(100)]
        public string? CrimeCommitted { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCommitted { get; set; }
        [StringLength(20)]
        public string Status { get; set; } = null!;

        public virtual Holder National { get; set; } = null!;
    }
}
