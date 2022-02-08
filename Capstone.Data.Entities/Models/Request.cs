using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("Request")]
    public partial class Request
    {
        [Key]
        public int RequestId { get; set; }
        public int RecordTypeId { get; set; }
        [StringLength(20)]
        public string RequestStatus { get; set; } = null!;
        [StringLength(200)]
        public string? RecordLink { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateRequested { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateApproved { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; } = null!;
        [StringLength(200)]
        public string? Remarks { get; set; }
        [StringLength(50)]
        public string? IssuedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateIssued { get; set; }
        [StringLength(50)]
        public string? VerifiedBy { get; set; }
        public int UserId { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; } = null!;
        [StringLength(100)]
        public string LastName { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }
        [StringLength(200)]
        public string? Purpose { get; set; }
        public int? HolderId { get; set; }

        public virtual Holder National { get; set; } = null!;

        [ForeignKey(nameof(RecordTypeId))]
        [InverseProperty("Requests")]
        public virtual RecordType RecordType { get; set; } = null!;
    }
}
