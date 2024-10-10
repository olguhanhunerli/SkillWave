using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface IAuthServices
    {
        Task RegisterUserAsync(int id,
            string user_name,
            string email,
            string password,
            string user_role,
            bool user_status,
            string firstName,
            string lastName);
        Task<Dictionary<string , string>> LoginUserAsync(string email, string password);
        Task<string> RefreshToken(string refreshToken);
          

    }
}
