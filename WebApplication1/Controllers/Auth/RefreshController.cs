namespace WebApplication1;

[Route("api/[controller]")]
[ApiController]
public class RefreshController : BaseController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;

    public RefreshController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
        => _userUnitOfWork = userUnitOfWork;
    
    [HttpPost]
    public async Task<IActionResult> Refresh(Token? refreshToken)
    {
        string oldToken = Request.Cookies["RefreshToken"] ?? string.Empty;

        if (refreshToken != null && refreshToken.RefreshToken != null)
            oldToken = refreshToken.RefreshToken;

        Token token = await _userUnitOfWork.Refresh(oldToken);

        ResponseResult<Token> response = new(token);

        SetCookie("AccessToken",
        token.AccessToken,
        token.AccessTokenExpiresAt);
        SetCookie("RefreshToken",
            token.RefreshToken,
            token.RefreshTokenExpiresAtExpires);

        return Ok(response);
    }

}
