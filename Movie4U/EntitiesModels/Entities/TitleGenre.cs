using Movie4U.EntitiesModels.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie4U.EntitiesModels.Entities
{
    public class TitleGenre: EntitiesModelsBase<TitleGenre,TitleGenreModel>
    {
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

        override public IdModel GetIds()
        {
            return new IdModel (genre_id, netflix_id);
        }

    }
}
