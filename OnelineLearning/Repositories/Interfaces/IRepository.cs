namespace OnelineLearning.Repositories.Interfaces
{
    //CRUD commun Pour les repositorie
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task SaveChangesAsync();
    }
}
