namespace Template.Web.Api.Dto;

public record NoteGetDto : GetDto
{
    public string Name { get; init; }
}