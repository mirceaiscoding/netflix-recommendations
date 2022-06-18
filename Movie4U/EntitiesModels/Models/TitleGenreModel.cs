using Movie4U.EntitiesModels.Entities;
using System;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Models
{
    public class TitleGenreModel: EntitiesModelsBase<TitleGenre, TitleGenreModel>, IModel<TitleGenreModel>
    {
        public static Expression<Func<TitleGenreModel, object>>[] idSelectors;

        static TitleGenreModel()
        {
            idSelectors = new Expression<Func<TitleGenreModel, object>>[]
            {
                entity => entity.genre_id,
                entity => entity.netflix_id
            };
        }


        public string genre { get; set; }

        public int genre_id { get; set; }

        public int netflix_id { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleGenreModel(TitleGenreModel source)
        {
            Copy(source);
        }

        /**<summary>
        * Constructor.
        * </summary>*/
        public TitleGenreModel(TitleGenre source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleGenreModel() { }

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

        public Expression<Func<TitleGenreModel, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
