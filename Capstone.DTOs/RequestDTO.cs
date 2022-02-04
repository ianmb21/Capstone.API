using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DTOs
{
    public class RequestDTO
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string NationalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string RequestStatus { get; set; }
        public int[] RecordTypeId { get; set; }
        public string? Remarks { get; set; }
        public int? HolderId { get; set; }
    }
}
