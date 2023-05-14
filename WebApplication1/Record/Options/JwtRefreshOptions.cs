namespace WebApplication1;

public class JwtRefreshOptions 
{
    public string? SecretKey { get; init; }
    public int ExpireTimeInMonths { get; init; }
}