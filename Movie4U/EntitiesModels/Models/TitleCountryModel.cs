using Movie4U.EntitiesModels.Entities;
using System;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Models
{
    public class TitleCountryModel: EntitiesModelsBase<TitleCountry, TitleCountryModel>, IModel<TitleCountryModel>
    {
        public static Expression<Func<TitleCountryModel, object>>[] idSelectors;

        static TitleCountryModel()
        {
            idSelectors = new Expression<Func<TitleCountryModel, object>>[]
            {
                entity => entity.country_id,
                entity => entity.netflix_id
            };
        }


        public int country_id { get; set; }  // while getting from uNoGs, it's name might not contain '_' for this class 

        public int netflix_id { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountryModel(TitleCountry source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountryModel(TitleCountryModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountryModel() { }

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

        public Expression<Func<TitleCountryModel, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
