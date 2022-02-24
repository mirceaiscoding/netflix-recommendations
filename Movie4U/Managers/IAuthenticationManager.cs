﻿using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public interface IAuthenticationManager
    {
        Task Signup(RegisterModel registerModel);
        Task<TokensModel> Login(LoginModel loginModel);
    }
}