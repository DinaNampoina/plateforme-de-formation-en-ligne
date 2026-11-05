using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetByCategoryIdAsync(int categoryId);
        Task<Course?> GetWithEnrollmentsAsync(int id);
    }
}