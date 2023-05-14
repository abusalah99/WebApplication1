namespace WebApplication1;

[Route("api/Admin/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BookController: BaseSettingsController<BookEntity>
{
    private readonly IBookUnitOfWork _unitOfWork;
    public BookController(IBookUnitOfWork unitOfWork)
        : base(unitOfWork) => _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] BookRequest BookRequest)
    {
        await _unitOfWork.Create(BookRequest);

        ResponseResult<string> response = new("Book created");
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromForm] BookRequest BookRequest)
    {
        await _unitOfWork.Update(BookRequest);

        ResponseResult<string> response = new("Book updated");
        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => await Remove(id);
}
