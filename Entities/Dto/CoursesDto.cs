using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class CoursesDto
    {
        public int CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseDuration { get; set; }
        public bool CourseStatus { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime? UploadAt { get; set; } = DateTime.UtcNow;
        public int? RetailPrice { get; set; }
        public int Price { get; set; }
        
    }
}
