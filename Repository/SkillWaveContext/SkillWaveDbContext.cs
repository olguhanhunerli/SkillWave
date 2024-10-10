using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SkillWaveContext
{
    public class SkillWaveDbContext :DbContext
    {
        public SkillWaveDbContext(DbContextOptions<SkillWaveDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasKey(u=> u.user_id);
            modelBuilder.Entity<Teachers>()
                .HasKey(t => t. teacher_id);
            modelBuilder.Entity<Teachers>()
                .HasOne(t => t.User)
                .WithOne()
                .HasForeignKey<Teachers>(t => t.user_id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Students>()
                .HasKey(s => s.student_id);
            modelBuilder.Entity<Students>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Students>(s => s.user_id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Enrollments>()
                .HasKey(e => new { e.course_id, e.student_id });
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Courses)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.course_id);
            modelBuilder.Entity<Enrollments>()
                .HasOne(c => c.Students)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(c => c.student_id);
            modelBuilder.Entity<Courses>()
                .HasKey(c => c.course_id);
        }
    }
}
