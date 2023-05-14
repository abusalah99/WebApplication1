namespace WebApplication1;

[Route("api/[controller]")]
[ApiController]
public class LogoutController : BaseController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public LogoutController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
            => _userUnitOfWork = userUnitOfWork;

    [HttpPost]
    public async Task<IActionResult> Logout(Token? refreshToken)
    {

        string oldToken = Request.Cookies["RefreshToken"] ?? string.Empty;

        if (refreshToken != null && refreshToken.RefreshToken != null)
            oldToken = refreshToken.RefreshToken;

        await _userUnitOfWork.Logout(oldToken);

        ResponseResult<string> response = new("Logout Sccess");

        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");
     
        return Ok(response);
    }
}
