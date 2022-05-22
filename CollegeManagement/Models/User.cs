using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Models
{
    public class User : Audit
    {
        public string EmailId { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
    }
}
