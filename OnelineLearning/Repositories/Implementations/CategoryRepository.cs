using Microsoft.EntityFrameworkCore;
using OnelineLearning.Data;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;
using OnelineLearning.DTOs;

namespace OnelineLearning.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(Guid categoryId)
        {
            return await _context.Courses
                .AsNoTracking()
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
        }
        public async Task<Pagination<Category>> PageCategoriesAsync(int pageNumber, int pageSize)
        {
            var query = _context.Categories.AsNoTracking();
            var totalCount = await query.CountAsync();
            var categories = await query
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new Pagination<Category>
            {
                Items = categories,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }
    }
}
