using System.ComponentModel.DataAnnotations;

namespace Movie4U.EntitiesModels.Entities
{
    public class TitleCountry
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

        virtual public Country Country { get; set; }
        virtual public Title title { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountry(string audio, string country, string country_code, string expire_date, string netflix_id, string new_date, string season_detail, string subtitle)
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

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountry() { }

    }
}
