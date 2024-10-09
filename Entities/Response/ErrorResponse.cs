using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response
{
    public class ErrorResponse
    {
        [DefaultValue(false)]
        public bool Success { get; set; }
        [DefaultValue("Invalid credentials")]
        public string Error { get; set; }
    }
}
