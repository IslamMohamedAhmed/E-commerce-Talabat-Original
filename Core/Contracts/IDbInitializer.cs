﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDbInitializer
    {
        public Task Initialize();
        public Task IdentityInitialize();
    }
}
