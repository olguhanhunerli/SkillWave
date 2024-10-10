using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Contract;
using Repository.SkillWaveContext;
using Service;
using Service.Contract;

namespace SkillWave.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SkillWaveDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),

            new MySqlServerVersion(new Version(8, 0, 23)))
            .EnableSensitiveDataLogging()  
               .LogTo(Console.WriteLine));
        }
       
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IAuthServices, AuthService>();
            services.AddScoped<ICourseService, CoursesService>();
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
        }
        
    }
}
