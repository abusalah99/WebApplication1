namespace WebApplication1;

public interface IJwtProvider
{
    string GenrateAccessToken(User user);
    string GenrateRefreshToken();

}
