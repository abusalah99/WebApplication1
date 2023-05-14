namespace WebApplication1;


public class BookUnitOfWork : BaseSettingsUnitOfWork<Book>, IBookUnitOfWork
{
    public BookUnitOfWork(IBookRepsitory repository, ILogger<BaseSettingsUnitOfWork<Book>> logger) 
        : base(repository, logger) { }
}