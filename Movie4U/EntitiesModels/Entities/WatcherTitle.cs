using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Movie4U.EntitiesModels.Entities
{
    public class WatcherTitle: EntitiesModelsBase<WatcherTitle,WatcherTitleModel>
    {
        static private Dictionary<int, Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>>> dynamicEntityFilters;

        static WatcherTitle()
        {
            dynamicEntityFilters = new Dictionary<int, Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>>>();
            dynamicEntityFilters.Add((int)WhereEnum.InWatchLater, source => ExpressionsUtility.propertyFilter(source, "watchLater", true));
            dynamicEntityFilters.Add((int)WhereEnum.NotInWatchLater, source => ExpressionsUtility.propertyFilter(source, "watchLater", false));
            dynamicEntityFilters.Add((int)WhereEnum.PrefferenceIsMore, source => ExpressionsUtility.propertyFilter(source, "preference", Preferences.More));
            dynamicEntityFilters.Add((int)WhereEnum.PrefferenceIsLess, source => ExpressionsUtility.propertyFilter(source, "preference", Preferences.Less));
            dynamicEntityFilters.Add((int)WhereEnum.PrefferenceIsNull, source => ExpressionsUtility.propertyFilter(source, "preference", Preferences.Null));

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
        
        public void Copy(WatcherTitleModelParameter source)
        {
            watcher_name = source.watcher_name;
            netflix_id = source.netflix_id;
            preference = source.preference;
            prefLastSetTime = source.prefLastSetTime;
            watchLater = source.watchLater;
            watchLaterLastSetTime = source.watchLaterLastSetTime;
        }

        override public IdModel GetId()
        {
            return new IdModel ( 2, watcher_name, netflix_id );
        }

        public override Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>> GetDynamicEntityFilter(int key)
        {
            if (!dynamicEntityFilters.TryGetValue(key, out Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>> filter))
                return null;
            return filter;
        }

    }
}
