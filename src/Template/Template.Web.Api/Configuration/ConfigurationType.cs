namespace Template.Web.Api.Configuration;

public class ConfigurationType
{
    public Type Entity { get; init; }
    public Type EditorDto { get; init; }
    public Type CreateDto { get; init; }
    public Type ViewDto { get; init; }
    public Type TableViewDto { get; set; }
}