using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("IdentityDetail")]
    public partial class IdentityDetail
    {
        [Key]
        public int IdentityDetailId { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; } = null!;
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [StringLength(50)]
        public string MiddleName { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [StringLength(10)]
        public string Gender { get; set; } = null!;

        public virtual Holder National { get; set; } = null!;
    }
}
