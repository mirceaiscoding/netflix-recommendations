using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class TitleGenres
    {
        public string genre { get; set; }

        [Required]
        public int genre_id { get; set; }

        [Required]
        public string netflix_id { get; set; }

        virtual public Genres Genre { get; set; }
        virtual public TitleDetails title { get; set; }


        // contructors:
        public TitleGenres(string genre, int genre_id, string netflix_id)
        {
            this.genre = genre;
            this.genre_id = genre_id;
            this.netflix_id = netflix_id;
        }

        public TitleGenres() { }

    }
}
