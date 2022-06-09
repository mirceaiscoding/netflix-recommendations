using System;
using System.Collections.Generic;
using Movie4U.EntitiesModels.Entities;

namespace Movie4U.EntitiesModels.Models
{
    public class WatcherTitleModel: EntitiesModelsBase<WatcherTitle,WatcherTitleModel>
    {

        static Dictionary<int, Func<WatcherTitleModel, WatcherTitleModel, int>> comparers;

        static WatcherTitleModel()
        {
            comparers = new Dictionary<int, Func<WatcherTitleModel, WatcherTitleModel, int>>();
            //sorters.Add(34, (wtm1, wtm2) => wtm1.prefference.CompareTo(wtm2.prefference) );

        }

        public string watcher_name { get; set; }

        public int netflix_id { get; set; }

        public WatcherTitle.Prefferences prefference { get; set; }

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

    }
}
