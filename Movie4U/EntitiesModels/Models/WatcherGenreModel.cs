using Movie4U.EntitiesModels.Entities;
using System;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Models
{
    public class WatcherGenreModel: EntitiesModelsBase<WatcherGenre, WatcherGenreModel>, IModel<WatcherGenreModel>
    {
        public static Expression<Func<WatcherGenreModel, object>>[] idSelectors;

        static WatcherGenreModel()
        {
            idSelectors = new Expression<Func<WatcherGenreModel, object>>[]
            {
                entity => entity.watcher_name,
                entity => entity.genre_id
            };
        }


        public string watcher_name { get; set; }

        public int genre_id { get; set; }

        public double watcherGenreScore { get; set; }

        public GenreModel genreModel { get; set; }


        /**<summary>
     * Constructor.
     * </summary>*/
        public WatcherGenreModel(WatcherGenre source, Genre genre)
        {
            Copy(source);
            genreModel = new GenreModel(genre);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenreModel(WatcherGenre source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenreModel(WatcherGenreModel source)
        {
            ShallowCopy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenreModel() { }

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

        override public void ShallowCopy(WatcherGenreModel source)
        {
            Copy(source);
            genreModel = source.genreModel;
        }

        public Expression<Func<WatcherGenreModel, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
