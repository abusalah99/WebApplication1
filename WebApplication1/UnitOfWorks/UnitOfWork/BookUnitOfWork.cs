namespace WebApplication1;


public class BookUnitOfWork : BaseSettingsUnitOfWork<Book>, IBookUnitOfWork
{
    public BookUnitOfWork(IBookRepsitory repository) : base(repository) { }

    public override async Task Create(Book book)
    {
        if (book == null)
            throw new ArgumentException("Book is not supplied");
        if (book.Author == null)
            throw new ArgumentException("Aurhor is required");
        if (book.Cover_Image == null)
            throw new ArgumentException("Cover Image is required");
        if (book.Title == null)
            throw new ArgumentException("Title is required");
       await base.Create(book);
    }

}