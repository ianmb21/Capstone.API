using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class EducationRecord
    {
        public int EducationRecordId { get; set; }
        public string NationalId { get; set; } = null!;
        public string LevelOfEducation { get; set; } = null!;
        public string SchoolName { get; set; } = null!;
        public int YearGraduated { get; set; }
        public string? Course { get; set; }

        public virtual Holder National { get; set; } = null!;
    }
}
