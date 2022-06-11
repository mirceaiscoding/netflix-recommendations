
namespace Movie4U.EntitiesModels.Models
{
    /**<summary>
     * A WatcherGenreModel without models lists. 
     * Used as parameter for POST and PUT requests.
     * </summary> */
    public class WatcherGenreModelParameter
    {
        public string watcher_name { get; set; }

        public int genre_id { get; set; }

        public double watcherGenreScore { get; set; }


        public WatcherGenreModelParameter(string watcher_name, int genre_id, double watcherGenreScore = 0)
        {
            this.watcher_name = watcher_name;
            this.genre_id = genre_id;
            this.watcherGenreScore = watcherGenreScore;
        }

        public WatcherGenreModelParameter() 
        {
            this.watcherGenreScore = 0;
        }

    }
}
