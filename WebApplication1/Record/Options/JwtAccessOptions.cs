namespace WebApplication1;

public class JwtAccessOptions
{
    public  string? Issuser { get; init; } 
    public  string? Audience { get; init; }
    public  string? SecretKey { get; init; }
    public int ExpireTimeInMintes { get; init; }
}