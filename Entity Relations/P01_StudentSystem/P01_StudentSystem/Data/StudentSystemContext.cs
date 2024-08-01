using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
            
        }

        public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
            : base(options) 
        {
            
        }

        public DbSet<Student>? Students { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Resource>? Resources { get; set; }
        public DbSet<StudentCourse>? StudentsCourses { get; set; }
        public DbSet<Homework>? Homeworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                string connectionString = "Server=DESKTOP-A8P7BPS\\SQLEXPRESS;Database=SoftUni;Integrated Security=true;TrustServerCertificate=true;";

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
