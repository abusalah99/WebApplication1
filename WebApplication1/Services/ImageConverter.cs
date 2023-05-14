namespace WebApplication1;

public class ImageConverter : IImageConverter
{
    public async Task<byte[]> ConvertImage(IFormFile image)
    {
        MemoryStream byteImage = new();
        await image.CopyToAsync(byteImage);
        return byteImage.ToArray();
    }
}