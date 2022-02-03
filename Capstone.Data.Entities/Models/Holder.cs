using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("Holder")]
    [Index(nameof(NationalId), Name = "UC_NationalId", IsUnique = true)]
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

        [Key]
        public int HolderId { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; } = null!;
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(50)]
        public string? MiddleName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }

        public virtual ICollection<CreditScore> CreditScores { get; set; }
        public virtual ICollection<CriminalRecord> CriminalRecords { get; set; }
        public virtual ICollection<EducationRecord> EducationRecords { get; set; }
        public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; }
        public virtual ICollection<IdentityDetail> IdentityDetails { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
