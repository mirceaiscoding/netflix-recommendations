using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class User : IdentityUser
    {
        public static object Identity { get; internal set; }
        public virtual Watcher watcher { get; set; }
    }
}
