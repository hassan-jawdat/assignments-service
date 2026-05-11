using Shiko.API.DTOs;
using Shiko.Domain.Entities;
using Shiko.Domain.Enums;
using Shiko.Domain.Interfaces;

namespace Shiko.API.Services;

public class AssignmentService
{
    private readonly IAssignmentRepository _assignmentRepo;
    private readonly ICourseRepository _courseRepo;

    public AssignmentService(IAssignmentRepository assignmentRepo, ICourseRepository courseRepo)
    {
        _assignmentRepo = assignmentRepo;
        _courseRepo = courseRepo;
    }

    public async Task<IEnumerable<AssignmentDto>> GetUserAssignmentsAsync(string userId)
    {
        var assignments = await _assignmentRepo.GetByUserIdAsync(userId);
        return assignments.Select(MapToDto);
    }

    public async Task<AssignmentDto?> GetUserAssignmentAsync(string userId, int courseId)
    {
        var assignment = await _assignmentRepo.GetByUserAndCourseAsync(userId, courseId);
        return assignment is null ? null : MapToDto(assignment);
    }

    public async Task<(AssignmentDto? result, string? error)> AssignCourseAsync(CreateAssignmentDto dto)
    {
        var course = await _courseRepo.GetByIdAsync(dto.CourseId);
        if (course is null)
            return (null, "Course not found.");

        if (await _assignmentRepo.ExistsAsync(dto.UserId, dto.CourseId))
            return (null, "User is already assigned to this course.");

        var assignment = new UserAssignment
        {
            UserId = dto.UserId,
            CourseId = dto.CourseId,
            Status = AssignmentStatus.InProgress,
            AssignedAt = DateTime.UtcNow
        };

        var created = await _assignmentRepo.AddAsync(assignment);
        created.Course = course;
        return (MapToDto(created), null);
    }

    public async Task<(AssignmentDto? result, string? error)> UpdateStatusAsync(string userId, int courseId, UpdateAssignmentStatusDto dto)
    {
        var assignment = await _assignmentRepo.GetByUserAndCourseAsync(userId, courseId);
        if (assignment is null)
            return (null, "Assignment not found.");

        if (!Enum.TryParse<AssignmentStatus>(dto.Status, true, out var status))
            return (null, "Invalid status value.");

        assignment.Status = status;
        if (status == AssignmentStatus.Completed)
            assignment.CompletedAt = DateTime.UtcNow;

        await _assignmentRepo.UpdateAsync(assignment);
        return (MapToDto(assignment), null);
    }

    private static AssignmentDto MapToDto(UserAssignment ua) => new()
    {
        Id = ua.Id,
        CourseId = ua.CourseId,
        CourseTitle = ua.Course.Title,
        CourseLevel = ua.Course.Level.ToString(),
        CourseHours = ua.Course.Hours,
        CourseIconUrl = ua.Course.IconUrl,
        Status = ua.Status.ToString(),
        AssignedAt = ua.AssignedAt,
        CompletedAt = ua.CompletedAt
    };
}
