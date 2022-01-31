using System;
using System.Collections.Generic;

namespace Capstone.Data.Entities.Models
{
    public partial class RecordType
    {
        public RecordType()
        {
            Requests = new HashSet<Request>();
        }

        public int RecordTypeId { get; set; }
        public string RecordName { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
