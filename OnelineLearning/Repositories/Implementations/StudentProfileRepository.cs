using Microsoft.EntityFrameworkCore;
using OnelineLearning.Data;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Repositories.Implementations
{
    public class StudentProfileRepository : Repository<StudentProfile>, IStudentProfileRepository
    {
        public StudentProfileRepository(ApplicationDbContext context) : base(context) { }

        public async Task<StudentProfile?> GetByStudentIdAsync(Guid studentId)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.StudentId == studentId);
        }
    }
}