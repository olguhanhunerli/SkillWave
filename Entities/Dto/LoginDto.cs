using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class LoginDto
    {
        [DefaultValue("user@example.com")]
        public string Email { get; set; }
        [DefaultValue("@Passw0rd123")]
        public string Password { get; set; }
    }
}
