using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class Country: EntitiesModelsBase<Country, CountryModel>, IEntity<Country>
    {
        public static Expression<Func<Country, object>>[]  idSelectors;

        static Country()
        {
            idSelectors = new Expression<Func<Country, object>>[1];
            idSelectors[0] = entity => entity.id;
        }


        [Required, Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public string country { get; set; }

        public string countrycode { get; set; }

        public int expiring { get; set; }

        public int nl7 { get; set; }

        public int tmovs { get; set; }

        public int tseries { get; set; }

        public int tvids { get; set; }

        virtual public List<TitleCountry> titleCountries { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public Country(Country source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Country(CountryModel source)
        {
            Copy(source);
        }

        public Country(CountryResponseModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Country() { }

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

        private void Copy(CountryResponseModel source)
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

        public Expression<Func<Country, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }

    }
}
