using Movie4U.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Models
{
    public class WatcherModel
    {
        public string watcher_name { get; set; }

        public DateTime register_date { get; set; }

        public string refreshToken { get; set; }

        public DateTime refreshTokenExpiryTime { get; set; }

        public string userId { get; set; }

        public void copy(Watcher source)
        {
            this.watcher_name = source.watcher_name;
            this.register_date = source.register_date;
            this.userId = source.userId;
            this.refreshToken = source.refreshToken;
            this.refreshTokenExpiryTime = source.refreshTokenExpiryTime;
        }
    }
}
