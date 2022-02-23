using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public interface IWatchersManager
    {
        List<WatcherModel> GetAll();

        WatcherModel GetWatcher(string name);

        Task Create(string watcherName, string UserId);

        /*Task Update();   // update will be implemented when more specific needs are detailed  */

        Task Delete(string name);
    }
}
