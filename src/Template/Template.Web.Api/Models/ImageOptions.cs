namespace Template.Web.Api.Models;

public class ImageOptions
{
    public long MaxLength { get; set; }
    public IEnumerable<string> AllowedTypes { get; set; }
}