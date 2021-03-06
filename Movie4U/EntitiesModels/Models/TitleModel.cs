using Movie4U.EntitiesModels.Entities;
using System.Collections.Generic;

namespace Movie4U.EntitiesModels.Models
{
    public class TitleModel : EntitiesModelsBase<Title, TitleModel>
    {

        public string title { get; set; }

        public string img { get; set; }

        public string title_type { get; set; }

        public int netflix_id { get; set; }

        public string synopsis { get; set; }

        public string rating { get; set; }

        public string year { get; set; }

        public string runtime { get; set; }

        public string poster { get; set; }

        public int top250 { get; set; }

        public int top250tv { get; set; }

        public string title_date { get; set; }

        public List<CountryModel> countryModels { get; set; }

        public List<GenreModel> genreModels { get; set; }

        public List<TitleImageModel> titleImageModels { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleModel(Title source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleModel(TitleModel source)
        {
            ShallowCopy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleModel() { }

        override public void Copy(Title source)
        {
            this.title = source.title;
            this.img = source.img;
            this.title_type = source.title_type;
            this.netflix_id = source.netflix_id;
            this.synopsis = source.synopsis;
            this.rating = source.rating;
            this.runtime = source.runtime;
            this.poster = source.poster;
            this.top250 = source.top250;
            this.top250tv = source.top250tv;
            this.title_date = source.title_date;
            this.year = source.year;
        }

        override public void Copy(TitleModel source)
        {
            this.title = source.title;
            this.img = source.img;
            this.title_type = source.title_type;
            this.netflix_id = source.netflix_id;
            this.synopsis = source.synopsis;
            this.rating = source.rating;
            this.runtime = source.runtime;
            this.poster = source.poster;
            this.top250 = source.top250;
            this.top250tv = source.top250tv;
            this.title_date = source.title_date;
            this.year = source.year;
        }

        override public void ShallowCopy(TitleModel source)
        {
            Copy(source);
            countryModels = source.countryModels;
            genreModels = source.genreModels;
            titleImageModels = source.titleImageModels;
        }

        override public IdModel GetId()
        {
            return new IdModel(1, netflix_id);
        }

    }
}
