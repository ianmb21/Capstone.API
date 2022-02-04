using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DTOs
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string NationalId { get; set; }
    }
}
