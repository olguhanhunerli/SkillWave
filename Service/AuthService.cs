using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Contract;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService : IAuthServices
    {
        public readonly IAuthRepository _repository;
        public readonly IConfiguration _configuration;
        public readonly IPasswordHasher<User> _hasher;


        public AuthService(IAuthRepository repository, IConfiguration configuration, IPasswordHasher<User> hasher)
        {
            _repository = repository;
            _configuration = configuration;
            _hasher = hasher;
        }

        public async Task<Dictionary<string , string>> LoginUserAsync(string email, string password)
        {
            var user = await _repository.GetUserByEmail(email);
            if (user == null || _hasher.VerifyHashedPassword(user,user.password_hash,password) == PasswordVerificationResult.Failed)
            {
                throw new Exception("Kullanıcı Adı Veya Şifresi Yanlış");
            }
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim ("sub", user.user_id.ToString()),
                    new Claim ("roleClaim", user.user_role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["TokenExpiryInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var refreshToken = GenerateRefreshToken();
            user.refresh_token = refreshToken;
            user.token_expirytime = DateTime.UtcNow.AddDays(7);
            await _repository.UpdateUser(user);
            return new Dictionary<string, string>
            {
                { "Token" , tokenString },
                { "RefreshToken" , refreshToken }
            };

        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) 
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string refreshToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out SecurityToken securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Geçersiz Token");
            }
            return principal;
        }
        

        public async Task RegisterUserAsync(int id,
            string user_name, 
            string email, 
            string password,
            string user_role, 
            bool user_status,
            string firstName,
            string lastName)
        {
            var existingUser = await _repository.GetUserByEmail(email);
            if (existingUser != null)
            {
                throw new NotImplementedException("Kullanıcı Zaten Mevcut");
            }
            var user = new User
            {
                user_id = id,
                user_name = user_name,
                password_hash = password,
                user_role = user_role,
                user_email = email,
                user_status = user_status
            };
            
            user.password_hash = _hasher.HashPassword(user, password);
            await _repository.AddUser(user);
            var userId = user.user_id;
            if (user_role == "Teacher".ToLower())
            {
                var teacher = new Teachers
                {
                    user_id = userId,
                    first_name = firstName,
                    last_name = lastName,
                    created_at = DateTime.UtcNow,

                };
                await _repository.AddTeacher(teacher);
            }
           else if (user_role == "Student".ToLower())
            {
                var student = new Students
                {
                    user_id = userId,
                    first_name = firstName,
                    last_name = lastName,
                    created_at = DateTime.UtcNow,
                };
                await _repository.AddStudent(student);
            }
        }

        public async Task<string> RefreshToken(string refreshToken)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var user = await _repository.RefreshToken(refreshToken);
            if (user == null || user.token_expirytime <= DateTime.Now)
            {
                throw new SecurityTokenException("Geçersiz veya süresi dolmuş refresh token");
            }
            var claims = new[]
            {
                new Claim("sub", user.user_id.ToString()),
                new Claim("role", user.user_role)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["TokenExpiryInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var newAccesToken = tokenHandler.WriteToken(token);

            var newRefreshToken = GenerateRefreshToken();
            user.refresh_token = newRefreshToken;
            user.token_expirytime = DateTime.UtcNow.AddDays(7);
            await _repository.UpdateUser(user);

            return newAccesToken;
        }
    }
}
