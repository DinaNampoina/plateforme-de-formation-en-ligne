using OnelineLearning.Data;
using Microsoft.EntityFrameworkCore;
namespace OnelineLearning.Repositories.Implementations
{
    public class Repository<T> where T : class
    {
        protected readonly ApplicationDbContext _Context;
        protected readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _Context = context;
            _dbSet = _Context.Set<T>();
        }
    }
}
