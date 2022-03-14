using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.EntitiesModels.Models
{
    /**<summary>
     * A WatcherGenreModel without models lists. 
     * Used as parameter for POST and PUT requests.
     * </summary> */
    public class WatcherGenreModelParameter
    {
        public string watcher_name { get; set; }

        public int genre_id { get; set; }

        public double watcherGenreScore { get; set; }
    
    }
}
