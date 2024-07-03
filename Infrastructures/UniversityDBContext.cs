using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace UniversityProject.Infrastructures
{
    public class UniversityDBContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public UniversityDBContext()
        {
        }

        public UniversityDBContext(DbContextOptions<UniversityDBContext> options)
            : base(options)
        {
        }


        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<UserCourse> UserCourses { get; set; }
        public virtual DbSet<ImageType> ImageType { get; set; }
        public virtual DbSet<CourseTime> CourseTime { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
       => optionsBuilder.UseSqlServer(" Data Source=185.55.224.39;Initial Catalog=ashkanri_UniversityDB; User Id=ashkanri_AdminUni; password=university123!@#; TrustServerCertificate=True;Integrated Security=false;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(uc => uc.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Image)
                .WithMany()
                .HasForeignKey(c => c.ImageId)
                .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Course>()
            //    .HasOne(c => c.CourseTime)
            //    .WithMany(d => d.Courses)
            //    .HasForeignKey(c => c.CourseTimeId)
            //    .OnDelete(DeleteBehavior.SetNull);  // Allows for a nullable DayAndTimeId

            modelBuilder.Entity<CourseTime>().HasData(
           new CourseTime
           {
               Id = Guid.NewGuid(), // Generate a new GUID for the default DayAndTime entry
               Day = DayOfWeek.Monday, // Set the default day
               Time = new TimeSpan(9, 0, 0), // Set the default time (e.g., 9:00 AM)
               IsDeleted = false // Set default value for IsDeleted
           });
        }

    }
}
