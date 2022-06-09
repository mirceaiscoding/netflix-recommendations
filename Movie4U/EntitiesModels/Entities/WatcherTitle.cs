using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie4U.EntitiesModels.Entities
{
    public class WatcherTitle: EntitiesModelsBase<WatcherTitle,WatcherTitleModel>
    {
        static Dictionary<int, Func<WatcherTitle, bool>> filters;

        static WatcherTitle()
        {
            filters = new Dictionary<int, Func<WatcherTitle, bool>>();
            filters.Add((int)WhereEnum.InWatchLater, wt => wt.watchLater == true);
            filters.Add((int)WhereEnum.NotInWatchLater, wt => wt.watchLater == false);
            filters.Add((int)WhereEnum.PrefferenceIsMore, wt => wt.prefference == Prefferences.More);
            filters.Add((int)WhereEnum.PrefferenceIsLess, wt => wt.prefference == Prefferences.Less);
            filters.Add((int)WhereEnum.PrefferenceIsNull, wt => wt.prefference == Prefferences.Null);

        }

        public enum Prefferences
        {
            Null = 0,
            More = 1,
            Less = 2
        };


        [Required]
        public string watcher_name { get; set; }

        [Required]
        public int netflix_id { get; set; }

        public Prefferences prefference { get; set; }

        public DateTime prefLastSetTime { get; set; }

        public bool watchLater { get; set; }

        public DateTime watchLaterLastSetTime { get; set; }

        virtual public Watcher watcher { get; set; }
        virtual public Title title { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitle(WatcherTitle source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitle(WatcherTitleModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitle(WatcherTitleModelParameter source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitle() { }

        override public void Copy(WatcherTitle source)
        {
            watcher_name = source.watcher_name;
            netflix_id = source.netflix_id;
            prefference = source.prefference;
            prefLastSetTime = source.prefLastSetTime;
            watchLater = source.watchLater;
            watchLaterLastSetTime = source.watchLaterLastSetTime;
        }

        override public void Copy(WatcherTitleModel source)
        {
            watcher_name = source.watcher_name;
            netflix_id = source.netflix_id;
            prefference = source.prefference;
            prefLastSetTime = source.prefLastSetTime;
            watchLater = source.watchLater;
            watchLaterLastSetTime = source.watchLaterLastSetTime;
        }
        
        public void Copy(WatcherTitleModelParameter source)
        {
            watcher_name = source.watcher_name;
            netflix_id = source.netflix_id;
            prefference = source.prefference;
            prefLastSetTime = source.prefLastSetTime;
            watchLater = source.watchLater;
            watchLaterLastSetTime = source.watchLaterLastSetTime;
        }

        override public IdModel GetId()
        {
            return new IdModel ( 2, watcher_name, netflix_id );
        }

        override public Func<WatcherTitle, bool> GetFilter(int key)
        {
            if(!filters.TryGetValue(key, out Func<WatcherTitle, bool> filter))
                return null;
            return filter;
        }

    }
}
