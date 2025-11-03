using Template.Services.Shared.Interfaces;

namespace Template.Services.Shared.Implementation;

internal class ImageService : IImageService
{
    public string ConvertFrom(MemoryStream memoryStream)
    {
        byte[] array = memoryStream.ToArray();
        return Convert.ToBase64String(array);
    }
}