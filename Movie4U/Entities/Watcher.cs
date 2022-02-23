using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class Watcher
    {
        [Required, Key]
        public string watcher_name { get; set; }

        public DateTime register_date { get; set; }

        [Required]
        public string userId { get; set; }

        public virtual User user { get; set; }
    }
}
