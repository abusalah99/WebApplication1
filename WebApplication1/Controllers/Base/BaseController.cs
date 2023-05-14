namespace WebApplication1;

[EnableCors("AllowAll")]
public class BaseController<TEntity> : ControllerBase
 where TEntity : BaseEntity
{
    private readonly IBaseUnitOfWork<TEntity> _unitOfWork;
    public BaseController(IBaseUnitOfWork<TEntity> unitOfWork) => _unitOfWork = unitOfWork;

    protected virtual async Task<IActionResult> Create(TEntity entity)
    {
        await _unitOfWork.Create(entity);

        ResponseResult<string> response = new($"{typeof(TEntity).Name} created");

        return Ok(response);
    }

    protected virtual async Task<IActionResult> Read()
    {

        IEnumerable<TEntity> entities = await _unitOfWork.Read();

        ResponseResult<IEnumerable<TEntity>> response = new(entities);

        return Ok(response);
    }

    protected async virtual Task<IActionResult> Update(TEntity entity)
    {
        await _unitOfWork.Update(entity);

        ResponseResult<string> response = new($"{typeof(TEntity).Name} Updated");

        return Ok(response);
    }

    protected async virtual Task<IActionResult> Remove(Guid id)
    {
        await _unitOfWork.Delete(id);

        ResponseResult<string> response = new($"{typeof(TEntity).Name} Deleted");

        return Ok(response);
    }
    protected void SetCookie(string name, string value, DateTime expireTime)
    => Response.Cookies.Append(name, value
        , new CookieOptions()
        {
            HttpOnly = true,
            Expires = expireTime
        });
}