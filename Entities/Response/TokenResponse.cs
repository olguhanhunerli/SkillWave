﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response
{
    public class TokenResponse
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
    }
}
