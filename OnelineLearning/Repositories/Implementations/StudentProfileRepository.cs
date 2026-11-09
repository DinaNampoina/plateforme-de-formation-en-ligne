using Microsoft.EntityFrameworkCore;
using OnelineLearning.Data;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;
using OnelineLearning.DTOs;

namespace OnelineLearning.Repositories.Implementations
{
    public class StudentProfileRepository : Repository<StudentProfile>, IStudentProfileRepository
    {
        public StudentProfileRepository(ApplicationDbContext context) : base(context) { }

        public async Task<StudentProfile?> GetByStudentIdAsync(Guid studentId)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.StudentId == studentId);
        }

        public async Task<Pagination<StudentProfile>> PageProfileAsync(int pageNumber, int pageSize)
        {
            var profil = _context.StudentProfiles.AsNoTracking();
            var totalCount = await profil.CountAsync();
            var students = await profil.OrderBy(s => s.BirthDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new Pagination<StudentProfile>
            {
                Items = students,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }
    }
}