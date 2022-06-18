using Movie4U.EntitiesModels.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class TitleGenre: EntitiesModelsBase<TitleGenre,TitleGenreModel>, IEntity<TitleGenre>
    {
        public static Expression<Func<TitleGenre, object>>[] idSelectors;

        static TitleGenre()
        {
            idSelectors = new Expression<Func<TitleGenre, object>>[2];
            idSelectors[0] = entity => entity.genre_id;
            idSelectors[1] = entity => entity.netflix_id;
        }


        public string genre { get; set; }

        [Required]
        public int genre_id { get; set; }

        [Required]
        public int netflix_id { get; set; }

        virtual public Genre Genre { get; set; }
        virtual public Title title { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleGenre(TitleGenre source)
        {
            Copy(source);
        }

        /**<summary>
        * Constructor.
        * </summary>*/
        public TitleGenre(TitleGenreModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleGenre() { }

        override public void Copy(TitleGenre source)
        {
            this.genre = source.genre;
            this.genre_id = source.genre_id;
            this.netflix_id = source.netflix_id;
        }

        public override void Copy(TitleGenreModel source)
        {
            this.genre = source.genre;
            this.genre_id = source.genre_id;
            this.netflix_id = source.netflix_id;
        }

        public Expression<Func<TitleGenre, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }

    }
}
