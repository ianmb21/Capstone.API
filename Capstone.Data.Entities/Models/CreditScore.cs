using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class CreditScore
    {
        public int CreditSoreId { get; set; }
        public string NationalId { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string? CreditStatus { get; set; }
        public string BankName { get; set; } = null!;
        public string ScoreRange { get; set; } = null!;

        public virtual Holder National { get; set; } = null!;
    }
}
