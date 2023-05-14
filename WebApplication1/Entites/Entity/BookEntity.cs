namespace WebApplication1;
public class BookEntity : BaseEntitySettings
{
    public string? Author { get; set; }
    public int Pages { get; set; }
    public DateTime ReleaseDate { get; set; }
    public byte[]? CoverImage { get; set; }

}
