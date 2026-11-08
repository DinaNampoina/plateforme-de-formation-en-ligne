using Microsoft.EntityFrameworkCore;
using OnelineLearning.Data;
using OnelineLearning.DTOs;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Repositories.Implementations
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Course>> GetByCategoryIdAsync(Guid categoryId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Course?> GetWithEnrollmentsAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.CourseId == id);
        }
        public async Task<Pagination<Course>> PageCoursAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses.AsNoTracking();
            var totalCount = await query.CountAsync();
            var courses = await query
                .OrderBy(c => c.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new Pagination<Course>
            {
                Items = courses,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }
    }
}
