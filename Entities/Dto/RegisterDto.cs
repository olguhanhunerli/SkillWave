using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class RegisterDto
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [DefaultValue("user@example.com")]
        public string Email { get; set; }
        [DefaultValue("UserName")]
        public string UserName { get; set; }
        [DefaultValue("@Passw0rd123")]
        public string Password { get; set; }
        [DefaultValue("Teacher/Student")]
        public string Role { get; set; }
        [JsonIgnore]
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        [DefaultValue(true)]
        public bool Status { get; set; }
        [DefaultValue("Olguhan")]
        public string FirstName { get; set; }
        [DefaultValue("Hünerli")]
        public string LastName { get; set; }
    }
}
