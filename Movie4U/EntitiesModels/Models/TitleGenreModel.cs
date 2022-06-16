using Movie4U.EntitiesModels.Entities;

namespace Movie4U.EntitiesModels.Models
{
    public class TitleGenreModel: EntitiesModelsBase<TitleGenre, TitleGenreModel>
    {
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

        override public IdModel GetIds()
        {
            return new IdModel(genre_id, netflix_id);
        }

    }
}
