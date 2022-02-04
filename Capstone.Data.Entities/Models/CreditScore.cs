using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("CreditScore")]
    public partial class CreditScore
    {
        [Key]
        public int CreditSoreId { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; } = null!;
        [StringLength(20)]
        public string AccountNumber { get; set; } = null!;
        [StringLength(20)]
        public string? CreditStatus { get; set; }
        [StringLength(50)]
        public string BankName { get; set; } = null!;
        [StringLength(20)]
        public string ScoreRange { get; set; } = null!;

        public virtual Holder National { get; set; } = null!;
    }
}
