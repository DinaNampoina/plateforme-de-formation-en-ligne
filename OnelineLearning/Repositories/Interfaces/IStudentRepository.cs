using OnelineLearning.DTOs;
using OnelineLearning.Models;
using OnelineLearning.Repositories.Implementations;

namespace OnelineLearning.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetByEmailAsync(string email);
        Task<Pagination<Student>> GetPagedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Student>> SearchByNameAsync(string name);
        

    }
}                                                                               