﻿using Movie4U.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITokensManager
    {
        Task<string> GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string ExtractUserName(string tokenHeader);
    }
}