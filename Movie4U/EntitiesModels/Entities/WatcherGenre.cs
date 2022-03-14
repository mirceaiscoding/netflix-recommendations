
using System.ComponentModel.DataAnnotations;

namespace Movie4U.EntitiesModels.Entities
{
    public class WatcherGenre
    {
        [Required]
        public string watcher_name { get; set; }

        [Required]
        public int genre_id { get; set; }

        public double score { get; set; }

        virtual public Watcher watcher { get; set; }
        virtual public Genre genre { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenre(string watcher_name, int genre_id)
        {
            this.watcher_name = watcher_name;
            this.genre_id = genre_id;
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenre() { }

    }
}
