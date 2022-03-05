using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class TitleCountries
    {
        public string audio { get; set; }

        public string country { get; set; }

        [Required]
        public string country_code { get; set; }

        public string expire_date { get; set; }

        [Required]
        public string netflix_id { get; set; }

        public string new_date { get; set; }

        public string season_detail { get; set; }

        public string subtitle { get; set; }

        virtual public Countries Country { get; set; }
        virtual public TitleDetails title { get; set; }


        // contructors:
        public TitleCountries(string audio, string country, string country_code, string expire_date, string netflix_id, string new_date, string season_detail, string subtitle)
        {
            this.audio = audio;
            this.country = country;
            this.country_code = country_code;
            this.expire_date = expire_date;
            this.netflix_id = netflix_id;
            this.new_date = new_date;
            this.season_detail = season_detail;
            this.subtitle = subtitle;
        }

        public TitleCountries() { }

    }
}
