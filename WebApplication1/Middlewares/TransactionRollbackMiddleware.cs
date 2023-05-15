namespace WebApplication1;

public class TransactionRollbackMiddleware : IMiddleware
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionRollbackMiddleware(ApplicationDbContext dbContext) => _dbContext = dbContext;
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        IDbContextTransaction transaction = null;
        try
        {
            transaction = await _dbContext.Database.BeginTransactionAsync();

            await next(context);

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            transaction?.Rollback();
            throw;
        }
    }
}
