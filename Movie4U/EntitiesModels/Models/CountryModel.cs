using Movie4U.EntitiesModels.Entities;
using System;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Models
{
    public class CountryModel : EntitiesModelsBase<Country, CountryModel>, IModel<CountryModel>
    {
        public static Expression<Func<CountryModel, object>>[] idSelectors;

        static CountryModel()
        {
            idSelectors = new Expression<Func<CountryModel, object>>[1];
            idSelectors[0] = model => model.id;
        }


        public int id { get; set; }

        public string country { get; set; }

        public string countrycode { get; set; }

        public int expiring { get; set; }

        public int nl7 { get; set; }

        public int tmovs { get; set; }

        public int tseries { get; set; }

        public int tvids { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public CountryModel(CountryModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public CountryModel(Country source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public CountryModel() { }

        override public void Copy(Country source)
        {
            id = source.id;
            country = source.country;
            countrycode = source.countrycode;
            expiring = source.expiring;
            nl7 = source.nl7;
            tmovs = source.tmovs;
            tseries = source.tseries;
            tvids = source.tvids;
        }

        override public void Copy(CountryModel source)
        {
            id = source.id;
            country = source.country;
            countrycode = source.countrycode;
            expiring = source.expiring;
            nl7 = source.nl7;
            tmovs = source.tmovs;
            tseries = source.tseries;
            tvids = source.tvids;
        }

        public Expression<Func<CountryModel, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }

    }
}
