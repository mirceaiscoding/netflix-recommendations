using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class Genre: EntitiesModelsBase<Genre, GenreModel>, IEntity<Genre>
    {
        public static Expression<Func<Genre, object>>[] idSelectors;

        static Genre()
        {
            idSelectors = new Expression<Func<Genre, object>>[1];
            idSelectors[0] = entity => entity.genre_id;
        }


        public string genre { get; set; }

        [Required, Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int genre_id { get; set; }

        virtual public List<TitleGenre> titleGenres { get; set; }
        public virtual List<WatcherGenre> watcherGenres { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public Genre(Genre source)
        {
            Copy(source);
        }

        /**<summary>
        * Constructor.
        * </summary>*/
        public Genre(GenreModel source)
        {
            Copy(source);
        }

        public Genre(GenreResponseModel source)
        {
            this.genre = source.genre;
            this.genre_id = source.netflix_id;
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Genre() { }

        override public void Copy(Genre source)
        {
            genre = source.genre;
            genre_id = source.genre_id;
        }

        override public void Copy(GenreModel source)
        {
            genre = source.genre;
            genre_id = source.genre_id;
        }

        public Expression<Func<Genre, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }

    }
}
