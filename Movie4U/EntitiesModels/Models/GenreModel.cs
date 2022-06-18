using Movie4U.EntitiesModels.Entities;
using System;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Models
{
    public class GenreModel: EntitiesModelsBase<Genre, GenreModel>, IModel<GenreModel>
    {
        public static Expression<Func<GenreModel, object>>[] idSelectors;

        static GenreModel()
        {
            idSelectors = new Expression<Func<GenreModel, object>>[]
            {
                entity => entity.genre_id
            };
        }


        public string genre { get; set; }

        public int genre_id { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public GenreModel(GenreModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public GenreModel(Genre source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public GenreModel() { }

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

        public Expression<Func<GenreModel, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
