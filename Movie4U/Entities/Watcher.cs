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

        public string refreshToken { get; set; }

        public DateTime refreshTokenExpiryTime { get; set; }

        [Required]
        public string userId { get; set; }

        public virtual User user { get; set; }
        public virtual List<WatcherTitle> watcherTitles {get; set;}


        /**<summary>
         * Constructor.
         * </summary>*/
        public Watcher(string watcher_name, DateTime register_date, string refreshToken, DateTime refreshTokenExpiryTime, string userId)
        {
            this.watcher_name = watcher_name;
            this.register_date = register_date;
            this.refreshToken = refreshToken;
            this.refreshTokenExpiryTime = refreshTokenExpiryTime;
            this.userId = userId;
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Watcher() { }

    }
}
