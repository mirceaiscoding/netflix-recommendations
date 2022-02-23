using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.Managers;
using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchersController : ControllerBase
    {
        private readonly IWatchersManager manager;

        public WatchersController(IWatchersManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("GetAllWatchers")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetAll()
        {
            var watchers = manager.GetAll();

            if (watchers.Count == 0)
                return NotFound("There are no watchers stored in the database");

            return Ok(watchers);
        }

        [HttpGet("GetWatcherByName/{name}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetWatcherByName([FromRoute] string name)
        {
            WatcherModel watcher = manager.GetWatcher(name);

            if (watcher == null)
                return NotFound("There is no watcher with the given name stored in the database");

            return Ok(watcher);
        }



    }
}
