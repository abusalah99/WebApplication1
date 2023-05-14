namespace WebApplication1;

public class BookRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }   
    public string? Auther { get; set; }
    public int Pages { get; set; }
    public DateTime ReleaseDate { get; set; }
    public IFormFile? CoverImage { get; set; }

}
