using Microsoft.EntityFrameworkCore;
using OnelineLearning.Models; 

namespace OnelineLearning.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(e =>
            {
                e.Property(s => s.FirstName).IsRequired().HasMaxLength(100);
                e.Property(s => s.LastName).IsRequired().HasMaxLength(100);
                e.Property(s => s.Email).IsRequired().HasMaxLength(255);
                e.Property(s => s.CreatedAt).IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<StudentProfile>(e =>
            {
                e.Property(sp => sp.PhoneNumber).HasMaxLength(20);
                e.Property(sp => sp.Address).HasMaxLength(255);
                e.HasOne(sp => sp.Student)
                 .WithOne(s => s.StudentProfile)
                 .HasForeignKey<StudentProfile>(sp => sp.StudentId)
                 .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Category>(e =>
            {
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
                e.Property(c => c.Description).HasMaxLength(500);
                e.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.Property(c => c.Title).IsRequired().HasMaxLength(200);
                e.Property(c => c.Price).HasPrecision(10, 2);
                e.HasOne(c => c.Category)
                 .WithMany(c => c.Courses)
                 .HasForeignKey(c => c.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Enrollment>(e =>
            {
                e.Property(e =>e.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
                e.Property(en => en.EnrollmentDate).IsRequired();

                e.HasOne(en => en.Student)
                 .WithMany(s => s.Enrollments)
                 .HasForeignKey(en => en.StudentId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(en => en.Course)
                 .WithMany(c => c.Enrollments)
                 .HasForeignKey(en => en.CourseId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(en => new { en.StudentId, en.CourseId }).IsUnique();
            });

            
        }
    }
}
