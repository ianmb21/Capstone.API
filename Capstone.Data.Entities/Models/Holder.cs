using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class Holder
    {
        public Holder()
        {
            CreditScores = new HashSet<CreditScore>();
            CriminalRecords = new HashSet<CriminalRecord>();
            EducationRecords = new HashSet<EducationRecord>();
            EmploymentHistories = new HashSet<EmploymentHistory>();
            IdentityDetails = new HashSet<IdentityDetail>();
            Requests = new HashSet<Request>();
        }

        public int HolderId { get; set; }
        public string NationalId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }

        public virtual ICollection<CreditScore> CreditScores { get; set; }
        public virtual ICollection<CriminalRecord> CriminalRecords { get; set; }
        public virtual ICollection<EducationRecord> EducationRecords { get; set; }
        public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; }
        public virtual ICollection<IdentityDetail> IdentityDetails { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
