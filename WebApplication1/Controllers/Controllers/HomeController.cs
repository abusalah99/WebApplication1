namespace WebApplication1;

[Route("api/[controller]")]
[ApiController]
public class HomeController : BaseSettingsController<Book>
{
    public HomeController(IBookUnitOfWork unitOfWork)
             : base(unitOfWork) { }

    [HttpGet]
    public async Task<IActionResult> Get() => await Read();

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => await Read(id);  

}
