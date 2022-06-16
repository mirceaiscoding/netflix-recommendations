
namespace Movie4U.EntitiesModels.Models
{
    /**<summary>
     * A WatcherGenreModel without models lists. 
     * Used as parameter for POST and PUT requests.
     * </summary> */
    public class WatcherGenreModelParameter
    {
        public int genre_id { get; set; }

        public double watcherGenreScore { get; set; }


        public WatcherGenreModelParameter(int genre_id, double watcherGenreScore = 0)
        {
            this.genre_id = genre_id;
            this.watcherGenreScore = watcherGenreScore;
        }

        public WatcherGenreModelParameter() 
        {
            this.watcherGenreScore = 0;
        }

    }
}
