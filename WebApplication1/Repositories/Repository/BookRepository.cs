namespace WebApplication1;

public class BookRepository : BaseRepositiorySettings<BookEntity>, IBookRepsitory
{
    public BookRepository(ApplicationDbContext context) : base(context) { }
}
