namespace WebApplication1;

public interface IImageConverter
{
    Task<byte[]> ConvertImage(IFormFile image);
}