namespace WebApplication1;

public class BaseSettingsController<TEntity> : BaseController<TEntity>
where TEntity : BaseEntitySettings
{
    private readonly IBaseSettingsUnitOfWork<TEntity> _baseSettingsUnitOfWork;
    public BaseSettingsController(IBaseSettingsUnitOfWork<TEntity> unitOfWork) : base(unitOfWork)
        => _baseSettingsUnitOfWork = unitOfWork;

    protected virtual async Task<IActionResult> Search([FromRoute] string searchText)
    {
        IEnumerable<TEntity> entities = await _baseSettingsUnitOfWork.Search(searchText);

        ResponseResult<IEnumerable<TEntity>> response = new(entities);

        return Ok(response);
    }
}