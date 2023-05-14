namespace WebApplication1;

[Route("api/Admin/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BookController: BaseSettingsController<Book>
{
    public BookController(IBookUnitOfWork unitOfWork) : base(unitOfWork) { }

    [HttpPost]
    public async Task<IActionResult> Post(Book book) => await Create(book);

    [HttpPut]
    public async Task<IActionResult> Put( Book book) => await Update(book);

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => await Remove(id);
}
