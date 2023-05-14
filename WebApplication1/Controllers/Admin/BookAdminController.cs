namespace WebApplication1;

[Route("api/Admin/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BookAdminController: BaseSettingsController<BookEntity>
{
    private readonly IBookUnitOfWork _unitOfWork;
    public BookAdminController(BookUnitOfWork unitOfWork)
        : base(unitOfWork) => _unitOfWork = unitOfWork;

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] BookRequest BookRequest)
    {
        await _unitOfWork.Create(BookRequest);

        ResponseResult<string> response = new("Home section created");
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromForm] BookRequest BookRequest)
    {
        await _unitOfWork.Update(BookRequest);

        ResponseResult<string> response = new("Home section updated");
        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => await Remove(id);
}
