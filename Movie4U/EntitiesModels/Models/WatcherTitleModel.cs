using System;
using System.ComponentModel.DataAnnotations;
using Movie4U.EntitiesModels.Entities;

namespace Movie4U.EntitiesModels.Models
{
    public class WatcherTitleModel
    {

        public string watcher_name { get; set; }

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
            watcher_name = source.watcher_name;
            netflix_id = netflix_id;
            prefference = prefference;
            prefLastSetTime = prefLastSetTime;
            watchLater = watchLater;
            watchLaterLastSetTime = watchLaterLastSetTime;
        }

    }
}
