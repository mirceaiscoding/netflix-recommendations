using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Movie4U.EntitiesModels.Entities;
using Movie4U.Enums;

namespace Movie4U.EntitiesModels.Models
{
    public class WatcherTitleModel: EntitiesModelsBase<WatcherTitle,WatcherTitleModel>
    {

        static private Dictionary<int, Func<WatcherTitleModel, WatcherTitleModel, int>> modelComparers;
        static private Dictionary<int, Func<IQueryable<WatcherTitleModel>, IQueryable<WatcherTitleModel>>> dynamicModelSorters;

        static WatcherTitleModel()
        {
            modelComparers = new Dictionary<int, Func<WatcherTitleModel, WatcherTitleModel, int>>();
            modelComparers.Add((int)OrderByEnum.Score, (wtm1, wtm2) => wtm1.GetScore().CompareTo(wtm2.GetScore()));
            
            dynamicModelSorters = new Dictionary<int, Func<IQueryable<WatcherTitleModel>, IQueryable<WatcherTitleModel>>>();
            dynamicModelSorters.Add((int)OrderByEnum.Score, query => scoreSorter(query));

        }
        
        public static IQueryable<TModel> scoreSorter<TModel>(IQueryable<TModel> source)
            where TModel : WatcherTitleModel
        {
            Type thisType = typeof(TModel);
            ParameterExpression wtm = Expression.Parameter(thisType, "wtm");
            MethodInfo getScoreMethodInfo = thisType.GetMethod("GetScore");

            var call = Expression.Call(wtm, getScoreMethodInfo);

            var accessor1 = Expression.PropertyOrField(wtm, "watcher_name");
            var accessor2 = Expression.PropertyOrField(wtm, "netflix_id");

            var lambda = Expression.Lambda < Func < TModel, double>>(call, false, new[] {wtm});
            var lambda1 = Expression.Lambda<Func<TModel, string>>(accessor1, false, new[] { wtm });
            var lambda2 = Expression.Lambda<Func<TModel, string>>(accessor2, false, new[] { wtm });

            return source
                .OrderBy(lambda)
                .ThenBy(lambda1)    // ordering by composite key in order to generate fully unique ordering (see https://docs.microsoft.com/en-us/ef/core/querying/single-split-queries)
                .ThenBy(lambda2);
        }

        public string watcher_name { get; set; }

        public int netflix_id { get; set; }

        public string title { get; set; }

        public WatcherTitle.Preferences preference { get; set; }

        public DateTime prefLastSetTime { get; set; }

        public bool watchLater { get; set; }

        public DateTime watchLaterLastSetTime { get; set; }

        public string synopsis { get; set; }

        public string rating { get; set; }

        public string year { get; set; }

        public string poster { get; set; }

        public List<CountryModel> countryModels { get; set; }

        public List<WatcherGenreModel> watcherGenreModels { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitleModel(WatcherTitle source) 
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitleModel(WatcherTitleModel source)
        {
            ShallowCopy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitleModel() { }

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

            synopsis = source.synopsis;
            rating = source.rating;
            year = source.year;
            poster = source.poster;
            title = source.title;
        }

        override public void ShallowCopy(WatcherTitleModel source)
        {
            Copy(source);
            countryModels = source.countryModels;
            watcherGenreModels = source.watcherGenreModels;
        }

        override public IdModel GetIds()
        {
            return new IdModel(watcher_name, netflix_id);
        }

        public override Func<WatcherTitleModel, WatcherTitleModel, int> GetModelComparer(int key)
        {
            if (!modelComparers.TryGetValue(key, out Func<WatcherTitleModel, WatcherTitleModel, int> comparer))
                return null;
            return comparer;
        }

        public override Func<IQueryable<WatcherTitleModel>, IQueryable<WatcherTitleModel>> GetDynamicModelSorter(int key)
        {
            if (!dynamicModelSorters.TryGetValue(key, out Func<IQueryable<WatcherTitleModel>, IQueryable<WatcherTitleModel>> sorter))
                return null;
            return sorter;
        }

        public double GetScore()
        {
            double score = 0;
            foreach(var watcherGenreModel in watcherGenreModels)
                score += watcherGenreModel.watcherGenreScore;

            switch (preference)
            {
                case WatcherTitle.Preferences.More:
                    score += 50.0 / ((DateTime.Now - prefLastSetTime).TotalDays + 1);
                    break;
                case WatcherTitle.Preferences.Less:
                    score -= 50.0 / ((DateTime.Now - prefLastSetTime).TotalDays + 1);
                    break;
            }

            if (watchLater)
                score += 10.0 / ((DateTime.Now - watchLaterLastSetTime).TotalDays + 1);

            return score;
        }

    }
}
