namespace WebApplication1;

[Route("api/[controller]")]
[ApiController]
public class HomeController : BaseSettingsController<BookEntity>
{
    public HomeController(IBookUnitOfWork unitOfWork)
             : base(unitOfWork) { }

    [HttpGet]
    public async Task<IActionResult> Get() => await Read();

}
