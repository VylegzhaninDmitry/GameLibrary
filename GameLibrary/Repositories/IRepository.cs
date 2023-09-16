namespace GameLibrary;

public interface IRepository<TEntity>
{
    Task SaveAsync(TEntity entity);

    Task<List<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(int id);

    void DeleteById(int id);

    void Delete(TEntity entity);

    void Update(TEntity entity);
}