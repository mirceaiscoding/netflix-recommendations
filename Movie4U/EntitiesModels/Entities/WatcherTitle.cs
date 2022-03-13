using System;
using System.ComponentModel.DataAnnotations;

namespace Movie4U.EntitiesModels.Entities
{
    public class WatcherTitle
    {
        public enum Prefferences
        {
            Null = 0,
            More = 1,
            Less = 2
        };


        [Required]
        public string watcher_name { get; set; }

        [Required]
        public string netflix_id { get; set; }

        public Prefferences prefference { get; set; }

        public DateTime prefLastSetTime { get; set; }

        bool watchLater { get; set; }

        DateTime watchLaterLastSetTime { get; set; }

        virtual public Watcher watcher { get; set; }
        virtual public Title title { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitle(string watcher_name, string netflix_id, Prefferences prefference, DateTime prefLastSetTime, bool watchLater, DateTime watchLaterLastSetTime)
        {
            this.watcher_name = watcher_name;
            this.netflix_id = netflix_id;
            this.prefference = prefference;
            this.prefLastSetTime = prefLastSetTime;
            this.watchLater = watchLater;
            this.watchLaterLastSetTime = watchLaterLastSetTime;
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitle() { }

    }
}
