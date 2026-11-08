using OnelineLearning.Models;
using OnelineLearning.DTOs;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {

        Task<Category?> GetByNameAsync(string name);
        Task<IEnumerable<Course>> GetCoursesAsync(Guid categoryId);
        Task<Pagination<Category>> PageCategoriesAsync(int pageNumber, int pageSize);

    }
}