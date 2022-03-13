using System.ComponentModel.DataAnnotations;

namespace Movie4U.EntitiesModels.Entities
{
    public class TitleGenre
    {
        public string genre { get; set; }

        [Required]
        public int genre_id { get; set; }

        [Required]
        public string netflix_id { get; set; }

        virtual public Genre Genre { get; set; }
        virtual public Title title { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleGenre(string genre, int genre_id, string netflix_id)
        {
            this.genre = genre;
            this.genre_id = genre_id;
            this.netflix_id = netflix_id;
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleGenre() { }

    }
}
