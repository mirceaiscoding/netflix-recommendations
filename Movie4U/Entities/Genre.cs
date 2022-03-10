using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class Genre
    {
        public string genre { get; set; }

        [Required, Key]
        public int genre_id { get; set; }

        virtual public List<TitleGenre> titleGenres { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public Genre(string genre, int genre_id)
        {
            this.genre = genre;
            this.genre_id = genre_id;
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Genre() { }

    }
}
