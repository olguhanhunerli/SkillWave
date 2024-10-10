using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
   public class CoursesController: ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllCourse()
        {
            var entity = await _service.GetAllCourses();
            return Ok(entity);
        }
        [Authorize(Roles = "Teacher")]
        [HttpPost("Add-Course(Teacher)")]
        public async Task<IActionResult> AddCourse(CoursesDto courseDto)
        {
            var teacherId = int.Parse(User.Claims.First(c => c.Type =="sub").Value);
            var course = new Courses
            {
                course_name = courseDto.CourseName,
                course_description = courseDto.CourseDescription,
                course_duration = courseDto.CourseDuration,
                course_status = courseDto.CourseStatus,
                created_at = DateTime.UtcNow,
                upload_at = DateTime.UtcNow,
                price = courseDto.Price,
                retail_price = courseDto.RetailPrice,
            };
            await _service.AddCourse(course);
            return Ok(course);  
        }
    }
}
