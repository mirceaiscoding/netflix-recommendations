using System;
using System.Collections.Generic;
using Movie4U.EntitiesModels.Entities;
using Movie4U.Enums;

namespace Movie4U.EntitiesModels.Models
{
    public class WatcherTitleModel: EntitiesModelsBase<WatcherTitle,WatcherTitleModel>
    {

        static Dictionary<int, Func<WatcherTitleModel, WatcherTitleModel, int>> comparers;

        static WatcherTitleModel()
        {
            comparers = new Dictionary<int, Func<WatcherTitleModel, WatcherTitleModel, int>>();
            comparers.Add((int)OrderByEnum.Score, (wtm1, wtm2) => wtm1.GetScore().CompareTo(wtm2.GetScore()));

        }

        public string watcher_name { get; set; }

        public int netflix_id { get; set; }

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

        public override Func<WatcherTitleModel, WatcherTitleModel, int> GetTModelComparer(int key)
        {
            if (!comparers.TryGetValue(key, out Func<WatcherTitleModel, WatcherTitleModel, int> comparer))
                return null;
            return comparer;
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
