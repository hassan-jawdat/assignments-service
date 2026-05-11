using Shiko.Domain.Entities;
using Shiko.Domain.Enums;

namespace Shiko.Domain.Interfaces;

public interface IAssignmentRepository
{
    Task<IEnumerable<UserAssignment>> GetByUserIdAsync(string userId);
    Task<UserAssignment?> GetByUserAndCourseAsync(string userId, int courseId);
    Task<UserAssignment> AddAsync(UserAssignment assignment);
    Task UpdateAsync(UserAssignment assignment);
    Task<bool> ExistsAsync(string userId, int courseId);
}
