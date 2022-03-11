using Movie4U.Entities;
using System.ComponentModel.DataAnnotations;

namespace Movie4U.Models
{
    public class CountryModel
    {
        public string country { get; set; }

        [Required, Key]
        public string country_code { get; set; }  // while getting from uNoGs, it's name might not contain '_' for this class 

        public int expiring { get; set; }

        public int nl7 { get; set; }

        public int tmovs { get; set; }

        public int tseries { get; set; }

        public int tvids { get; set; }

        public void Copy(Country source)
        {
            this.country = source.country;
            this.country_code = source.country_code;
            this.expiring = source.expiring;
            this.nl7 = source.nl7;
            this.tmovs = source.tmovs;
            this.tseries = source.tseries;
            this.tvids = source.tvids;
        }

    }
}
