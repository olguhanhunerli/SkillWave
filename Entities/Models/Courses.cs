namespace Entities.Models
{
    public class Courses
    {
        public int course_id { get; set; }
        public int? teacher_id { get; set; }
        public ICollection<Teachers> Teachers { get; set; }
        public int? student_id { get; set; }
        public ICollection<Students> Students { get; set; }
        public string course_name { get; set; }
        public string course_description { get; set; }
        public string course_duration { get; set; }
        public bool course_status { get; set; }
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? upload_at { get; set; } = DateTime.UtcNow;
        public int? retail_price { get; set; }
        public int price { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; }
    }
}
