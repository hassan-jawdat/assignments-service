using Shiko.Domain.Enums;

namespace Shiko.Domain.Entities;

public class UserAssignment
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public AssignmentStatus Status { get; set; } = AssignmentStatus.NotStarted;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public Course Course { get; set; } = null!;
}
