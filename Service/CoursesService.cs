using Entities.Dto;
using Entities.Models;
using Repository.Contract;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CoursesService : ICourseService
    {
        private readonly ICourseRepository _repository;

        public CoursesService(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task AddCourse(Courses course)
        {
            await _repository.AddAsync(course);
        }

        public async Task<IEnumerable<CoursesDto>> GetAllCourses()
        {
           var courses = await _repository.GetAll();
           return courses.Select(c => new CoursesDto 
           {
               CourseId = c.course_id,
               CourseName = c.course_name,
               CourseDescription = c.course_description,
               CourseDuration = c.course_duration,
               CourseStatus = c.course_status,
               CreatedAt = DateTime.UtcNow,
               Price = c.price,
               RetailPrice = c.retail_price,
               StudentId = c.student_id,
               TeacherId = c.teacher_id,
               
           }).ToList();
        }
    }
}
