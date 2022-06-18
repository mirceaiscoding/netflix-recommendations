using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using Movie4U.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class WatcherTitle: EntitiesModelsBase<WatcherTitle,WatcherTitleModel>, IEntity<WatcherTitle>
    {
        static private Dictionary<int, Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>>> dynamicFilters;
        public static Expression<Func<WatcherTitle, object>>[] idSelectors;

        static WatcherTitle()
        {
            dynamicFilters = new Dictionary<int, Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>>>();
            dynamicFilters.Add((int)WhereEnum.InWatchLater, source => source.PropertyFilter("watchLater", true));
            dynamicFilters.Add((int)WhereEnum.NotInWatchLater, source => source.PropertyFilter("watchLater", false));
            dynamicFilters.Add((int)WhereEnum.PrefferenceIsMore, source => source.PropertyFilter("preference", Preferences.More));
            dynamicFilters.Add((int)WhereEnum.PrefferenceIsLess, source => source.PropertyFilter("preference", Preferences.Less));
            dynamicFilters.Add((int)WhereEnum.PrefferenceIsNull, source => source.PropertyFilter("preference", Preferences.Null));

            idSelectors = new Expression<Func<WatcherTitle, object>>[]
            {
                entity => entity.watcher_name,
                entity => entity.netflix_id
            };
        }

        public enum Preferences
        {
            Null = 0,
            More = 1,
            Less = 2
        };


        [Required]
        public string watcher_name { get; set; }

        [Required]
        public int netflix_id { get; set; }

        public Preferences preference { get; set; }

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
        public WatcherTitle(string watcherName, WatcherTitleModelParameter source)
        {
            Copy(watcherName, source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitle() { }

        override public void Copy(WatcherTitle source)
        {
            watcher_name = source.watcher_name;
            netflix_id = source.netflix_id;
            preference = source.preference;
            prefLastSetTime = source.prefLastSetTime;
            watchLater = source.watchLater;
            watchLaterLastSetTime = source.watchLaterLastSetTime;
        }

        override public void Copy(WatcherTitleModel source)
        {
            watcher_name = source.watcher_name;
            netflix_id = source.netflix_id;
            preference = source.preference;
            prefLastSetTime = source.prefLastSetTime;
            watchLater = source.watchLater;
            watchLaterLastSetTime = source.watchLaterLastSetTime;
        }
        
        public void Copy(string watcherName, WatcherTitleModelParameter source)
        {
            watcher_name = watcherName;
            netflix_id = source.netflix_id;
            preference = source.preference;
            prefLastSetTime = source.prefLastSetTime;
            watchLater = source.watchLater;
            watchLaterLastSetTime = source.watchLaterLastSetTime;
        }

        public Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>> GetDynamicFilter(int key)
        {
            if (!dynamicFilters.TryGetValue(key, out Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>> filter))
                return null;
            return filter;
        }

        public Expression<Func<WatcherTitle, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
