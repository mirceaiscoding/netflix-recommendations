using Movie4U.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Models
{
    public class GenreModel
    {
        public string genre { get; set; }

        [Required, Key]
        public int genre_id { get; set; }
    
        public void Copy(Genre source)
        {
            this.genre = source.genre;
            this.genre_id = source.genre_id;
        }

    }
}
