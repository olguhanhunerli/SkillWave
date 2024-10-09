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
    public class AuthRepository : IAuthRepository
    {
        private readonly SkillWaveDbContext _context;

        public AuthRepository(SkillWaveDbContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Students student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task AddTeacher(Teachers teacher)
        {
           
            
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>u.user_email == email);
        }
    }
}
