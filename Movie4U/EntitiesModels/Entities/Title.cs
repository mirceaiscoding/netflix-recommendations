using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie4U.EntitiesModels.Entities
{
    public class Title
    {
        public string alt_id { get; set; }

        public string alt_image { get; set; }

        public string alt_metascore { get; set; }

        public string alt_plot { get; set; }

        public string alt_runtime { get; set; }

        public string alt_votes { get; set; }

        public string awards { get; set; }

        public string default_image { get; set; }

        public string large_image { get; set; }

        public string latest_date { get; set; }

        public string maturity_label { get; set; }

        public string maturity_level { get; set; }

        [Required, Key]
        public string netflix_id { get; set; }

        public string origin_country { get; set; }

        public string poster { get; set; }

        public string rating { get; set; }

        public string runtime { get; set; }

        public string start_date { get; set; }

        public string synopsis { get; set; }

        public string title { get; set; }

        public string title_type { get; set; }

        public string year { get; set; }

        virtual public List<TitleCountry> titleCountries { get; set; }
        virtual public List<TitleGenre> titleGenres { get; set; }
        virtual public List<TitleImage> titleImages { get; set; }
        public virtual List<WatcherTitle> watcherTitles { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public Title(string alt_id, string alt_image, string alt_metascore, string alt_plot, string alt_runtime, string alt_votes, string awards, string default_image, string large_image, string latest_date, string maturity_label, string maturity_level, string netflix_id, string origin_country, string poster, string rating, string runtime, string start_date, string synopsis, string title, string title_type, string year)
        {
            this.alt_id = alt_id;
            this.alt_image = alt_image;
            this.alt_metascore = alt_metascore;
            this.alt_plot = alt_plot;
            this.alt_runtime = alt_runtime;
            this.alt_votes = alt_votes;
            this.awards = awards;
            this.default_image = default_image;
            this.large_image = large_image;
            this.latest_date = latest_date;
            this.maturity_label = maturity_label;
            this.maturity_level = maturity_level;
            this.netflix_id = netflix_id;
            this.origin_country = origin_country;
            this.poster = poster;
            this.rating = rating;
            this.runtime = runtime;
            this.start_date = start_date;
            this.synopsis = synopsis;
            this.title = title;
            this.title_type = title_type;
            this.year = year;
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Title() { }

    }
}
