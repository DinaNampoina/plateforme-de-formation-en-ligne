using Microsoft.EntityFrameworkCore;
using OnelineLearning.Data;
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
    }
}
