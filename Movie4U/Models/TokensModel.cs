using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Models
{
    public class TokensModel
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }

        public void copy(TokensModel source)
        {
            this.accessToken = source.accessToken;
            this.refreshToken = source.refreshToken;
        }
    }
}
