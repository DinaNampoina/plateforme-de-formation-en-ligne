using OnelineLearning.Models;
using OnelineLearning.Repositories.Implementations;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetByEmailAsync(string email);
    }
}