using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string user_email { get; set; }
        public string user_name { get; set; }
        public string password_hash { get; set; }
        public string user_role { get; set; }
        public string? refresh_token { get; set; }
        public DateTime? token_expirytime { get; set; } = DateTime.UtcNow;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public bool user_status { get; set; }

    }
}
