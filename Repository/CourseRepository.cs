using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contract;
using Repository.SkillWaveContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CourseRepository : ICourseRepository
    {
        public readonly SkillWaveDbContext _context;

        public CourseRepository(SkillWaveDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Courses entity)
        {
            await _context.Courses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Courses>> GetAll()
        {
            return await _context.Courses.ToListAsync();
        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Courses> UpdateAsync(Courses entity)
        {
            throw new NotImplementedException();
        }
    }
}
