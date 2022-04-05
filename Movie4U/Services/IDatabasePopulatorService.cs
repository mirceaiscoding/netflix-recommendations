using System;
using System.Threading.Tasks;

namespace Movie4U.Services
{
    public interface IDatabasePopulatorService
    {
        public Task CreateGenresAsync();
    }
}
