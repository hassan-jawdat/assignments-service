using Shiko.Domain.Enums;

namespace Shiko.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public CourseLevel Level { get; set; }
    public int Hours { get; set; }
    public string? IconUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UserAssignment> UserAssignments { get; set; } = new List<UserAssignment>();
}
