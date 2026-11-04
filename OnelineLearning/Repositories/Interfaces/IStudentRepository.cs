using OnelineLearning.Models;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student?> GetByIdAsync(int id);

        Task<Student?> GetByEmailAsync(string email);

        Task AddAsync(Student student);

        Task UpdateAsync(Student student);

        Task DeleteAsync(Student student);

        Task SaveChangesAsync();
    }
}