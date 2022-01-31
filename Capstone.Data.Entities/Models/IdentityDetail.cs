using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class IdentityDetail
    {
        public int IdentityDetailId { get; set; }
        public string NationalId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;

        public virtual Holder National { get; set; } = null!;
    }
}
