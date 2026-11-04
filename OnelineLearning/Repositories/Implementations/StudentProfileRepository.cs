using Microsoft.EntityFrameworkCore;
using OnelineLearning.Data;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Interfaces;

namespace OnelineLearning.Repositories.Implementations
{
    public class StudentProfileRepository : IStudentProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentProfile>> GetAllAsync()
        {
            return await _context.StudentProfiles.AsTracking().ToListAsync();
        }

        public async Task<StudentProfile?> GetByIdAsync(int id)
        {
            return await _context.StudentProfiles.FindAsync(id);
        }

        public async Task<StudentProfile?> GetByStudentIdAsync(int studentId)
        {
            return await _context.StudentProfiles.AsTracking().FirstOrDefaultAsync(p => p.StudentId == studentId);
        }

        public async Task AddAsync(StudentProfile profile)
        {
            await _context.StudentProfiles.AddAsync(profile);
        }

        public void Update(StudentProfile profile)
        {
            _context.StudentProfiles.Update(profile);
        }

        public void Delete(StudentProfile profile)
        {
            _context.StudentProfiles.Remove(profile);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}