using UniversityRegistrar.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace UniversityRegistrar.Models
{
    public class UniversityRegistrarContext : IdentityDbContext<User>
    {

        public UniversityRegistrarContext(
            DbContextOptions<UniversityRegistrarContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureStudents(modelBuilder);
            ConfigureLecturers(modelBuilder);
            ConfigureStudentCourses(modelBuilder);

        }

        public void ConfigureStudents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Faculty)
                .WithMany(f => f.Students)
                .HasForeignKey(s => s.FacultyId);

        }

        public void ConfigureLecturers(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Lecturer>()
                .HasOne(l => l.User)
                .WithOne(u => u.Lecturer)
                .HasForeignKey<Lecturer>(l => l.UserId);

            modelBuilder.Entity<Lecturer>()
                .HasOne(l => l.Faculty)
                .WithMany(f => f.Lecturers)
                .HasForeignKey(l => l.FacultyId);

            modelBuilder.Entity<Lecturer>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lecturers)
                .HasForeignKey(l => l.CourseId);
        }

        public void ConfigureStudentCourses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}