namespace WebApplication1;

public class UserRequest
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public int Age { get; set; }
    public string? FristName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public IFormFile? UserImage { get; set; }
    public string? ImageExtention { get; set; }  
}
