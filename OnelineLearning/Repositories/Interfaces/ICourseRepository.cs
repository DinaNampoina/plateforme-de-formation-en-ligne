using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();

        Task<Course?> GetByIdAsync(int id);

        Task<IEnumerable<Course>> GetByCategoryIdAsync(int categoryId);

        Task<Course?> GetWithEnrollmentsAsync(int id);

        Task AddAsync(Course course);

        void Update(Course course);

        void Delete(Course course);

        Task SaveChangesAsync();
    }
}