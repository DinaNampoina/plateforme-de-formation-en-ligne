using Microsoft.EntityFrameworkCore;
using OnelineLearning.Data;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;
using OnelineLearning.DTOs;

namespace OnelineLearning.Repositories.Implementations
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext context) : base(context) { }

       
        public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(Guid studentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        
        public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(e => e.Student)
                .Where(e => e.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public async Task<Pagination<Enrollment>> PageEnrollmentsAsync(int pageNumber, int pageSize)
        {
            var query = _context.Enrollments.AsNoTracking();
            var totalCount = await query.CountAsync();
            var enrollments = await query
                .OrderBy(e => e.EnrollmentId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new Pagination<Enrollment>
            {
                Items = enrollments,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }
    }
}
