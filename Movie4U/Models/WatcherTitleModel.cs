using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Movie4U.Entities;

namespace Movie4U.Models
{
    public class WatcherTitleModel
    {

        [Required]
        public string watcher_name { get; set; }

        [Required]
        public string netflix_id { get; set; }

        public WatcherTitle.Prefferences prefference { get; set; }

        public DateTime prefLastSetTime { get; set; }

        bool watchLater { get; set; }

        DateTime watchLaterLastSetTime { get; set; }

        //TitleDetailsModel title { get; set; }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitleModel() { }

        public void Copy(WatcherTitle source)
        {
            this.watcher_name = source.watcher_name;
            this.netflix_id = netflix_id;
            this.prefference = prefference;
            this.prefLastSetTime = prefLastSetTime;
            this.watchLater = watchLater;
            this.watchLaterLastSetTime = watchLaterLastSetTime;
        }

    }
}
