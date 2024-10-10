using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmail(string email);
        Task AddUser (User user);   
        Task AddTeacher(Teachers teacher);  
        Task AddStudent(Students student);
        Task<User> RefreshToken(string token);
        Task UpdateUser(User user);
    }
}
