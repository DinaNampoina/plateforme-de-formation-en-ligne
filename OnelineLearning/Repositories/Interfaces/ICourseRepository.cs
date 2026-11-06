using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetByCategoryIdAsync(Guid categoryId);
        Task<Course?> GetWithEnrollmentsAsync( Guid id);
    }
}