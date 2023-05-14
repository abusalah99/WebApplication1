

namespace WebApplication1;

public class User : BaseEntity
{
    public required string Email { get; set; }
    public string? Password { get; set; }
    public int Age { get; set; }
    public string? FristName { get; set; }
    public string? LastName { get; set; }    
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; }    
    public string? Role { get; set; }
    public byte[]? UserImage { get; set; }
    public string? ImageExtention { get; set; } 
    [JsonIgnore]
    public RefreshToken? Token { get; set; }
}