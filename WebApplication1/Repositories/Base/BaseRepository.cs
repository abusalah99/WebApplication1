namespace WebApplication1;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;
    protected DbSet<TEntity> dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        dbSet = _context.Set<TEntity>();
    }

    public virtual async Task Add(TEntity entity)
    {
        ThrowExceptionIfParameterNotSupplied(entity);

        await dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public virtual async Task Remove(Guid id)
    {
        TEntity entityFromDb =  await ThrowExceptionIfEntityNotExistsInDatabase(id);

        await Task.Run(() => dbSet.Remove(entityFromDb));
        await SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> Get() => await dbSet.ToListAsync();

    public virtual async Task<TEntity> Get(Guid id)
        => await dbSet.FirstOrDefaultAsync(e => e.Id == id);

    public virtual async Task Update(TEntity entity)
    {
        ThrowExceptionIfParameterNotSupplied(entity);
        await ThrowExceptionIfEntityNotExistsInDatabase(entity);

        await Task.Run(() => dbSet.Update(entity));
        await SaveChangesAsync();
    }

    protected async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    private static void ThrowExceptionIfParameterNotSupplied(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentException($"{typeof(TEntity).Name} was not provided.");
    }

    protected async Task ThrowExceptionIfEntityNotExistsInDatabase(TEntity entity)
    {
        TEntity? entityFromDb = await Get(entity.Id);
        if (entityFromDb == null)
            throw new ArgumentException($"{typeof(TEntity).Name} was not found in DB");
    }

    protected async Task<TEntity> ThrowExceptionIfEntityNotExistsInDatabase(Guid id)
    {
        TEntity? entityFromDb = await Get(id);
        if (entityFromDb == null)
            throw new ArgumentException($"{typeof(TEntity).Name} was not found in DB");

        return entityFromDb;
    }

}