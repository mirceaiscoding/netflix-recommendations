
using Movie4U.EntitiesModels.Models;
using System.Collections.Generic;

namespace Movie4U.Utilities
{
    public static class PreferenceScoreUtility
    {
        /**<summary>
         * Calculates the WatcherTitle score based on the model's retrieved data.
         * </summary> */
        public static double GetScore(WatcherTitleModel watcherTitle)
        {
            // genres doesn't contain the genreScore, because it's only the genral genre information. 
            // to obtain the genreScore, it should be done a join to WatcherGenres - probably in the WatcherTitleModel.
            // in the case, we'll end up having the TitleModel containing genreScore too, which is not a bad thing.
            // but, when a genreScore gets modified, how to modify all the TitleModels genreScore? 
            // I suppose we could recalculate all the scores all over again (just in the backend, not in the database, there is no genreScore field into WatcherTitles).
            // to be determined: When do we update the genreScore in the database and recalculate the WatcherTitle overall scores.

            return 0;
        }
    }
}
