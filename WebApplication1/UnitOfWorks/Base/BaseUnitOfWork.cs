namespace WebApplication1;

public class BaseUnitOfWork<TEntity> : IBaseUnitOfWork<TEntity> where TEntity : BaseEntity
{
    private readonly IBaseRepository<TEntity> _repository;
    private readonly ILogger<BaseUnitOfWork<TEntity>> _logger;
    public BaseUnitOfWork(IBaseRepository<TEntity> repository,
        ILogger<BaseUnitOfWork<TEntity>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<IEnumerable<TEntity>> Read() => await _repository.Get();
    public virtual async Task<TEntity> Read(Guid id) => await _repository.Get(id);

    public virtual async Task Create(TEntity entity)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();

        try
        {
            await _repository.Add(entity);
        }
        catch (Exception exception)
        {
            transaction.Rollback();

            _logger.LogError(exception.Message);
        }

        await transaction.CommitAsync();
    }
    public virtual async Task Delete(Guid id)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();

        try
        {
            await _repository.Remove(id);
        }
        catch (Exception exception)
        {
            transaction.Rollback();

            _logger.LogError(exception.Message);
        }

        await transaction.CommitAsync();
    }
    public virtual async Task Update(TEntity entity)
    {
        using IDbContextTransaction transaction = await _repository.GetTransaction();

        try
        {
            await _repository.Update(entity);
        }
        catch (Exception exception)
        {
            transaction.Rollback();

            _logger.LogError(exception.Message);
        }

        await transaction.CommitAsync();
    }

}