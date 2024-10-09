using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Enrollments
    {
        public int course_id { get; set; }
        public Courses Courses { get; set; }
        public int student_id { get; set; }
        public Students Students { get; set; }
    }
}
