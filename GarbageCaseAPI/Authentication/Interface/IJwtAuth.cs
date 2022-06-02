﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageCaseAPI.Authentication.Interface
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
