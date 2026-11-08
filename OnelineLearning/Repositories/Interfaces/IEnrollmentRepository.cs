using OnelineLearning.Models;
using OnelineLearning.DTOs;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(Guid studentId);
        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId);
        Task<Enrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId);
        Task<Pagination<Enrollment>> PageEnrollmentsAsync(int pageNumber, int pageSize);
    }
}