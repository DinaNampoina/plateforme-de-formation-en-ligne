using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId);
        Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId);
    }
}