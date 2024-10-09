using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Students
    {

        public int student_id { get; set; }
        public int user_id { get; set; }
        public User User { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public ICollection<Enrollments> Enrollments { get; set; }
    }
}
