using GameLibrary.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary;

public class EntityFrameworkRepository<TEntity> : IEntityFrameworkRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppContext _context;

    public EntityFrameworkRepository(AppContext context) =>
        _context = context;

    public async Task SaveAsync(TEntity entity) => 
        await _context.Set<TEntity>().AddAsync(entity);

    public async Task<List<TEntity>> GetAllAsync() => 
        await _context.Set<TEntity>().AsNoTracking().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(int id) =>
        await _context.Set<TEntity>().FindAsync(id);

    public void DeleteById(int id) => 
        _context.Set<BaseEntity>().Remove(new BaseEntity { Id = id });

    public void Delete(TEntity entity) => 
        _context.Set<TEntity>().Remove(entity);

    public void Update(TEntity entity) => 
        _context.Set<TEntity>().Update(entity);

    public IQueryable<TEntity> GetQueryable() =>
        _context.Set<TEntity>().AsQueryable();

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}