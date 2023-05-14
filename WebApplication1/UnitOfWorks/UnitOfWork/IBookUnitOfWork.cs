namespace WebApplication1;

public interface IBookUnitOfWork : IBaseSettingsUnitOfWork<Book>
{
    Task Create(BookRequest bookHomeRequest);
    Task Update(BookRequest bookHomeRequest);
}


