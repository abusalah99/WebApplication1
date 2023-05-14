namespace WebApplication1;

public interface IBaseSettingsUnitOfWork<TEntity> : IBaseUnitOfWork<TEntity>
     where TEntity : BaseEntitySettings
{
    Task<IEnumerable<TEntity>> Search(string searchText);

}