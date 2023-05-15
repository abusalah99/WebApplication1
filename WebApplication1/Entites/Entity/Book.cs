using System.Globalization;

namespace WebApplication1;
public class Book : BaseEntitySettings
{
    public string? Author { get; set; }
    public int Pages { get; set; }
    public string? Release_Date { get; set; }
    public string? Cover_Image { get; set; }

}
