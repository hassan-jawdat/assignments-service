using Microsoft.EntityFrameworkCore;
using Shiko.Domain.Entities;
using Shiko.Domain.Interfaces;
using Shiko.Infrastructure.Data;

namespace Shiko.Infrastructure.Repositories;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ShikoDbContext _context;

    public AssignmentRepository(ShikoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserAssignment>> GetByUserIdAsync(string userId)
    {
        return await _context.UserAssignments
            .Include(ua => ua.Course)
            .Where(ua => ua.UserId == userId)
            .OrderByDescending(ua => ua.AssignedAt)
            .ToListAsync();
    }

    public async Task<UserAssignment?> GetByUserAndCourseAsync(string userId, int courseId)
    {
        return await _context.UserAssignments
            .Include(ua => ua.Course)
            .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.CourseId == courseId);
    }

    public async Task<UserAssignment> AddAsync(UserAssignment assignment)
    {
        _context.UserAssignments.Add(assignment);
        await _context.SaveChangesAsync();
        return assignment;
    }

    public async Task UpdateAsync(UserAssignment assignment)
    {
        _context.UserAssignments.Update(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string userId, int courseId)
    {
        return await _context.UserAssignments
            .AnyAsync(ua => ua.UserId == userId && ua.CourseId == courseId);
    }
}
