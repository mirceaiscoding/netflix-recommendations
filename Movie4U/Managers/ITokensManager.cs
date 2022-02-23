using Movie4U.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public interface ITokensManager
    {
        Task<string> GenerateToken(User user);
        Task<string> ExtractUserName(string tokenHeader);
    }
}
