using Microsoft.EntityFrameworkCore;
using Shiko.Domain.Entities;
using Shiko.Domain.Interfaces;
using Shiko.Infrastructure.Data;

namespace Shiko.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ShikoDbContext _context;

    public CourseRepository(ShikoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task<Course> AddAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }
}
