using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class CriminalRecord
    {
        public int CriminalRecordId { get; set; }
        public string NationalId { get; set; } = null!;
        public string? CrimeCommitted { get; set; }
        public DateTime? DateCommitted { get; set; }
        public string Status { get; set; } = null!;

        public virtual Holder National { get; set; } = null!;
    }
}
