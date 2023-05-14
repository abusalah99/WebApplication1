namespace WebApplication1;

public interface IBookUnitOfWork : IBaseSettingsUnitOfWork<BookEntity>
{
    Task Create(BookRequest bookHomeRequest);
    Task Update(BookRequest bookHomeRequest);
}


