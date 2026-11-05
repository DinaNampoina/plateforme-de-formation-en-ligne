using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {

        Task<Category?> GetByNameAsync(string name);
        Task<IEnumerable<Course>> GetCoursesAsync(int categoryId);

    }
}