using Microsoft.EntityFrameworkCore;
using Shiko.Domain.Entities;

namespace Shiko.Infrastructure.Data;

public class ShikoDbContext : DbContext
{
    public ShikoDbContext(DbContextOptions<ShikoDbContext> options) : base(options) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<UserAssignment> UserAssignments => Set<UserAssignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Title).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<UserAssignment>(e =>
        {
            e.HasKey(ua => ua.Id);
            e.HasIndex(ua => new { ua.UserId, ua.CourseId }).IsUnique();
            e.HasOne(ua => ua.Course)
             .WithMany(c => c.UserAssignments)
             .HasForeignKey(ua => ua.CourseId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Design Accessibility", Level = Domain.Enums.CourseLevel.Advanced, Hours = 12 },
            new Course { Id = 2, Title = "Figma for Beginner", Level = Domain.Enums.CourseLevel.Intermediate, Hours = 16 },
            new Course { Id = 3, Title = "Framer Design", Level = Domain.Enums.CourseLevel.Advanced, Hours = 22 },
            new Course { Id = 4, Title = "Frontend Development", Level = Domain.Enums.CourseLevel.Intermediate, Hours = 14 },
            new Course { Id = 5, Title = "Behance Case Study", Level = Domain.Enums.CourseLevel.Intermediate, Hours = 16 }
        );
    }
}
