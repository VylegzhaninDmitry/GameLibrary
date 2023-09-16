namespace GameLibrary;

public interface IEntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetQueryable();

    Task SaveChangesAsync();
}