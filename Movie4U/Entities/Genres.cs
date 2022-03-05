using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class Genres
    {
        public string genre { get; set; }

        [Required, Key]
        public int genre_id { get; set; }

        virtual public List<TitleGenres> titleGenres { get; set; }


        // contructors:
        public Genres(string genre, int genre_id)
        {
            this.genre = genre;
            this.genre_id = genre_id;
        }

        public Genres() { }

    }
}
