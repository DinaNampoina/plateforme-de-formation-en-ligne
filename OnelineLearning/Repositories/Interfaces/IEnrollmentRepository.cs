using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();

        Task<Enrollment?> GetByIdAsync(int id);

        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);

        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId);

        Task<Enrollment?> GetByStudentAndCourseAsync(int studentId, int courseId);

        Task AddAsync(Enrollment enrollment);

        void Update(Enrollment enrollment);

        void Delete(Enrollment enrollment);

        Task SaveChangesAsync();
    }
}