using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class WatcherTitle: EntitiesModelsBase<WatcherTitle,WatcherTitleModel>
    {
        static private Dictionary<int, Func<WatcherTitle, bool>> entityFilters;
        static private Dictionary<int, Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>>> dynamicEntityFilters;

        static WatcherTitle()
        {
            entityFilters = new Dictionary<int, Func<WatcherTitle, bool>>();
            entityFilters.Add((int)WhereEnum.InWatchLater, wt => wt.watchLater == true);
            entityFilters.Add((int)WhereEnum.NotInWatchLater, wt => wt.watchLater == false);
            entityFilters.Add((int)WhereEnum.PrefferenceIsMore, wt => wt.preference == Preferences.More);
            entityFilters.Add((int)WhereEnum.PrefferenceIsLess, wt => wt.preference == Preferences.Less);
            entityFilters.Add((int)WhereEnum.PrefferenceIsNull, wt => wt.preference == Preferences.Null);

        }

        public static IQueryable<TEntity> propertyFilter<TEntity>(IQueryable<TEntity> source, string propertyName, object valueToMatch)
        where TEntity : WatcherTitle
        {
            var param = Expression.Parameter(typeof(TEntity));

            var accessor = Expression.PropertyOrField(param, propertyName);
            if(accessor == null)
                return source;

            var constant = Expression.Constant(valueToMatch);
            // should add a check if valueToMatch's type is the same as accessor's
            var equals = Expression.Equal(accessor, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, false, param);
            return source.Where(lambda);
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

        override public Func<WatcherTitle, bool> GetEntityFilter(int key)
        {
            if(!entityFilters.TryGetValue(key, out Func<WatcherTitle, bool> filter))
                return null;
            return filter;
        }

        public override Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>> GetDynamicEntityFilter(int key)
        {
            if (!dynamicEntityFilters.TryGetValue(key, out Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>> filter))
                return null;
            return filter;
        }

    }
}
