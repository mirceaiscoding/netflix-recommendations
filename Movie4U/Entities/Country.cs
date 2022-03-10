using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class Country
    {
        public string country { get; set; }

        [Required, Key]
        public string country_code { get; set; }  // while getting from uNoGs, it's name might not contain '_' for this class 

        public int expiring { get; set; }

        public int nl7 { get; set; }

        public int tmovs { get; set; }

        public int tseries { get; set; }

        public int tvids { get; set; }

        virtual public List<TitleCountry> titleCountries { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public Country(string country, string country_code, int expiring, int nl7, int tmovs, int tseries, int tvids)
        {
            this.country = country;
            this.country_code = country_code;
            this.expiring = expiring;
            this.nl7 = nl7;
            this.tmovs = tmovs;
            this.tseries = tseries;
            this.tvids = tvids;
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Country() { }

    }
}
