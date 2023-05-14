namespace WebApplication1;

public class BaseSettingsUnitOfWork<TEntity> : BaseUnitOfWork<TEntity>
    , IBaseSettingsUnitOfWork<TEntity> where TEntity : BaseEntitySettings
{
    private readonly IBaseRepositiorySettings<TEntity> _baseRepositiorySettings;
    public BaseSettingsUnitOfWork(IBaseRepositiorySettings<TEntity> repository,
        ILogger<BaseSettingsUnitOfWork<TEntity>> logger) : base(repository, logger) =>
    _baseRepositiorySettings = repository;

    public virtual async Task<IEnumerable<TEntity>> Search(string searchText) =>
        await _baseRepositiorySettings.Search(searchText);

}
