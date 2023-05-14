namespace WebApplication1;

[Route("api/[controller]")]
[ApiController]

public class RegisterController : BaseController<User>
{
    private readonly IUserUnitOfWork _userUnitOfWork;
    public RegisterController(IUserUnitOfWork userUnitOfWork) : base(userUnitOfWork)
            => _userUnitOfWork = userUnitOfWork;
   
    [HttpPost]
    public async Task<IActionResult> Post(User user) 
    {
        Token token = await _userUnitOfWork.Register(user);

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