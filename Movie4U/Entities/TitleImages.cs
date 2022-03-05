using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class TitleImages
    {
        public string image_type { get; set; }

        [Required]
        public string netflix_id { get; set; }

        [Required, Key]
        public string url { get; set; }        // "filmid as netflixid,url,itype"

        virtual public TitleDetails title { get; set; }


        // contructors:
        public TitleImages(string image_type, string netflix_id, string url)
        {
            this.image_type = image_type;
            this.netflix_id = netflix_id;
            this.url = url;
        }

        public TitleImages() { }

    }
}
