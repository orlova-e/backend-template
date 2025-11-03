using MimeTypes;

namespace Template.Web.Api.Services.Implementation;

public static class MimeTypeMappingService
{
    public static string MapFromFileName(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return MimeTypeMap.GetMimeType(extension);
    }
}