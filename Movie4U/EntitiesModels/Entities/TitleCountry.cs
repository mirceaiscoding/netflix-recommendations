using Movie4U.EntitiesModels.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie4U.EntitiesModels.Entities
{
    public class TitleCountry: EntitiesModelsBase<TitleCountry, TitleCountryModel>
    {
        [Required]
        public int country_id { get; set; }

        [Required]
        public int netflix_id { get; set; }

        virtual public Country Country { get; set; }
        virtual public Title title { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountry(TitleCountry source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountry(TitleCountryModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountry() { }

        override public void Copy(TitleCountry source)
        {
            country_id = source.country_id;
            netflix_id = source.netflix_id;
        }

        override public void Copy(TitleCountryModel source)
        {
            country_id = source.country_id;
            netflix_id = source.netflix_id;
        }

        override public IdModel GetIds()
        {
            return new IdModel (country_id, netflix_id);
        }

    }
}
