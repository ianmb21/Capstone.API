using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("RecordType")]
    public partial class RecordType
    {
        public RecordType()
        {
            Requests = new HashSet<Request>();
        }

        [Key]
        public int RecordTypeId { get; set; }
        [StringLength(50)]
        public string RecordName { get; set; } = null!;

        [InverseProperty(nameof(Request.RecordType))]
        public virtual ICollection<Request> Requests { get; set; }
    }
}
