using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class EmploymentHistory
    {
        public int EmploymentHistoryId { get; set; }
        public string NationalId { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public DateTime DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }
        public string Position { get; set; } = null!;

        public virtual Holder National { get; set; } = null!;
    }
}
