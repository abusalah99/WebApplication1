namespace WebApplication1;

public class BookRepository : BaseRepositiorySettings<Book>, IBookRepsitory
{
    public BookRepository(ApplicationDbContext context) : base(context) { }
}
