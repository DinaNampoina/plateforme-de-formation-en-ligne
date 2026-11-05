using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IStudentProfileRepository : IRepository<StudentProfile>
    {
        Task<StudentProfile?> GetByStudentIdAsync(int studentId);
    }
}