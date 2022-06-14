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
            ParameterExpression wtm1 = Expression.Parameter(thisType, "wtm1");
            ParameterExpression wtm2 = Expression.Parameter(thisType, "wtm2");
            MethodInfo getScoreMethodInfo = thisType.GetMethod("GetScore");

            ParameterExpression score1 = Expression.Parameter(typeof(double), "score1");
            ParameterExpression score2 = Expression.Parameter(typeof(double), "score2");
            MethodInfo compareToMethodInfo = typeof(double).GetMethod("CompareTo", new[] { typeof(double) });
            ParameterExpression difference = Expression.Parameter(typeof(int), "difference");

            BlockExpression block = Expression.Block(
                new[] { difference },
                Expression.Assign(score1, Expression.Call(wtm1, getScoreMethodInfo)),
                Expression.Assign(score2, Expression.Call(wtm2, getScoreMethodInfo)),
                Expression.Assign(difference, Expression.Call(score1, compareToMethodInfo, new[] { score2 })));

            var lambda = Expression.Lambda < Func < TModel, int>>(block, false, new[] {wtm1, wtm2});
            return source.OrderBy(lambda);
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

        override public IdModel GetId()
        {
            return new IdModel(2, watcher_name, netflix_id);
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
