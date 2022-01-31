using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public int RecordTypeId { get; set; }
        public string RequestStatus { get; set; } = null!;
        public string? RecordLink { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime? DateApproved { get; set; }
        public string NationalId { get; set; } = null!;
        public string? Remarks { get; set; }
        public string? IssuedBy { get; set; }
        public DateTime? DateIssued { get; set; }
        public string? VerifiedBy { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Birthdate { get; set; }

        public virtual Holder National { get; set; } = null!;
        public virtual RecordType RecordType { get; set; } = null!;
    }
}
