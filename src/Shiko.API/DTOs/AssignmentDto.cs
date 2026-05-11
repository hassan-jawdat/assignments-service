namespace Shiko.API.DTOs;

public class AssignmentDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string CourseLevel { get; set; } = string.Empty;
    public int CourseHours { get; set; }
    public string? CourseIconUrl { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class CreateAssignmentDto
{
    public string UserId { get; set; } = string.Empty;
    public int CourseId { get; set; }
}

public class UpdateAssignmentStatusDto
{
    public string Status { get; set; } = string.Empty;
}
