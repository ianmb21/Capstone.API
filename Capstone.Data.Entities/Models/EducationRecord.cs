using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data.Entities.Models
{
    [Table("EducationRecord")]
    public partial class EducationRecord
    {
        [Key]
        public int EducationRecordId { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; } = null!;
        [StringLength(50)]
        public string LevelOfEducation { get; set; } = null!;
        [StringLength(150)]
        public string SchoolName { get; set; } = null!;
        public int YearGraduated { get; set; }
        [StringLength(100)]
        public string? Course { get; set; }

        public virtual Holder National { get; set; } = null!;
    }
}
