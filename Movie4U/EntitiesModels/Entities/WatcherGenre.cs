using Movie4U.EntitiesModels.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class WatcherGenre: EntitiesModelsBase<WatcherGenre, WatcherGenreModel>, IEntity<WatcherGenre>
    {
        public static Expression<Func<WatcherGenre, object>>[] idSelectors;

        static WatcherGenre()
        {
            idSelectors = new Expression<Func<WatcherGenre, object>>[2];
            idSelectors[0] = entity => entity.watcher_name;
            idSelectors[1] = entity => entity.genre_id;
        }


        [Required]
        public string watcher_name { get; set; }

        [Required]
        public int genre_id { get; set; }

        public double watcherGenreScore { get; set; }

        virtual public Watcher watcher { get; set; }
        virtual public Genre genre { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenre(WatcherGenre source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenre(WatcherGenreModel source)
        {
            Copy(source);
        }

        public WatcherGenre(string watcherName, WatcherGenreModelParameter source)
        {
            Copy(watcherName, source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenre() { }

        override public void Copy(WatcherGenre source)
        {
            watcher_name = source.watcher_name;
            genre_id = source.genre_id;
            watcherGenreScore = source.watcherGenreScore;
        }

        override public void Copy(WatcherGenreModel source)
        {
            watcher_name = source.watcher_name;
            genre_id = source.genre_id;
            watcherGenreScore = source.watcherGenreScore;
        }

        public void Copy(string watcherName, WatcherGenreModelParameter source)
        {
            watcher_name = watcherName;
            genre_id = source.genre_id;
            watcherGenreScore = source.watcherGenreScore;
        }

        public Expression<Func<WatcherGenre, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
