using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.EntitiesModels
{
    public class IdModel
    {
        public int count { get; set; }

        public object id1 { get; set; }

        public object id2 { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public IdModel(int count = 0, object id1 = null, object id2 = null)
        {
            this.count = count;
            this.id1 = id1;
            this.id2 = id2;
        }

    }
}
