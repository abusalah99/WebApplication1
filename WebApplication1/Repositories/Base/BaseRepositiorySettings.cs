namespace WebApplication1;

public class BaseRepositiorySettings<TEntity> : BaseRepository<TEntity>
    , IBaseRepositiorySettings<TEntity> where TEntity : BaseEntitySettings
{
    public BaseRepositiorySettings(ApplicationDbContext context) : base(context) { }

    public virtual async Task<IEnumerable<TEntity>> Search(string searchText) =>
        await Task.Run(() => dbSet.Where(e => e.Title.Contains(searchText)));

}
