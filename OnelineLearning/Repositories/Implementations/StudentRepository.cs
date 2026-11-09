using Microsoft.EntityFrameworkCore;
using OnelineLearning.Data;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;
using OnelineLearning.DTOs;

namespace OnelineLearning.Repositories.Implementations
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Student?> GetByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(s => s.Email == email);
        }
        public async Task<Pagination<Student>> GetPagedAsync(int pageNumber,int pageSize)
        {
            var query = _context.Students.AsNoTracking();
            var totalCount = await query.CountAsync();

            var students = await query
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Pagination<Student>
            {
                Items = students,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public async Task<IEnumerable<Student>> SearchByNameAsync(string name)
        {
            name = name.ToLower();

            return await _context.Students  .AsNoTracking()
                .Where(s =>s.FirstName.ToLower().Contains(name) ||s.LastName.ToLower().Contains(name))
                .ToListAsync();
        }

    }
}
