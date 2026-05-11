using Microsoft.AspNetCore.Mvc;
using Shiko.API.DTOs;
using Shiko.API.Services;

namespace Shiko.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly AssignmentService _service;

    public AssignmentsController(AssignmentService service)
    {
        _service = service;
    }

    // GET api/assignments/{userId}
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserAssignments(string userId)
    {
        var assignments = await _service.GetUserAssignmentsAsync(userId);
        return Ok(assignments);
    }

    // GET api/assignments/{userId}/courses/{courseId}
    [HttpGet("{userId}/courses/{courseId}")]
    public async Task<IActionResult> GetUserAssignment(string userId, int courseId)
    {
        var assignment = await _service.GetUserAssignmentAsync(userId, courseId);
        if (assignment is null)
            return NotFound();
        return Ok(assignment);
    }

    // POST api/assignments
    [HttpPost]
    public async Task<IActionResult> AssignCourse([FromBody] CreateAssignmentDto dto)
    {
        var (result, error) = await _service.AssignCourseAsync(dto);
        if (error is not null)
            return BadRequest(new { message = error });
        return CreatedAtAction(nameof(GetUserAssignment),
            new { userId = dto.UserId, courseId = dto.CourseId }, result);
    }

    // PATCH api/assignments/{userId}/courses/{courseId}/status
    [HttpPatch("{userId}/courses/{courseId}/status")]
    public async Task<IActionResult> UpdateStatus(string userId, int courseId, [FromBody] UpdateAssignmentStatusDto dto)
    {
        var (result, error) = await _service.UpdateStatusAsync(userId, courseId, dto);
        if (error is not null)
            return error == "Assignment not found." ? NotFound() : BadRequest(new { message = error });
        return Ok(result);
    }
}
