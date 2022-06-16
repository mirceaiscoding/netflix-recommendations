using Movie4U.EntitiesModels.Entities;
using System;

namespace Movie4U.EntitiesModels.Models
{
    /**<summary>
     * A WatcherTitleModel without models lists. 
     * Used as parameter for POST and PUT requests.
     * </summary> */
    public class WatcherTitleModelParameter
    {
        public int netflix_id { get; set; }

        public WatcherTitle.Preferences preference { get; set; }

        public DateTime prefLastSetTime { get; set; }

        public bool watchLater { get; set; }

        public DateTime watchLaterLastSetTime { get; set; }


        public WatcherTitleModelParameter(int netflix_id, WatcherTitle.Preferences prefference, DateTime prefLastSetTime, bool watchLater, DateTime watchLaterLastSetTime)
        {
            this.netflix_id = netflix_id;
            this.preference = prefference;
            this.prefLastSetTime = prefLastSetTime;
            this.watchLater = watchLater;
            this.watchLaterLastSetTime = watchLaterLastSetTime;
        }

        public WatcherTitleModelParameter()
        {
            this.preference = WatcherTitle.Preferences.Null;
            this.watchLater = false;
        }

    }
}
