using Entities.Dto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface ICourseService
    {
        Task<IEnumerable<CoursesDto>> GetAllCourses();
        Task AddCourse(Courses course);
    }
}
