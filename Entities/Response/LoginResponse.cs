﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        [DefaultValue("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9")]
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        [DefaultValue("")]
        public string Error { get; set; }
    }
}
