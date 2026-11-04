using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IStudentProfileRepository
    {
        Task<IEnumerable<StudentProfile>> GetAllAsync();

        Task<StudentProfile?> GetByIdAsync(int id);

        Task<StudentProfile?> GetByStudentIdAsync(int studentId);

        Task AddAsync(StudentProfile profile);

        void Update(StudentProfile profile);

        void Delete(StudentProfile profile);

        Task SaveChangesAsync();
    }
}