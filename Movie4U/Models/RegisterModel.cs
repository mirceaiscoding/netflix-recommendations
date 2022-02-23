using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Models
{
    public class RegisterModel
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get;  set; }
        public string role { get; set; }
    }
}
