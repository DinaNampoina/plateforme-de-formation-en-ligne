using OnelineLearning.DTOs;
using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IStudentProfileRepository : IRepository<StudentProfile>
    {
        Task<StudentProfile?> GetByStudentIdAsync(Guid studentId);
        Task<Pagination<StudentProfile>> PageProfileAsync(int pageNumber, int pageSize);
    }
}