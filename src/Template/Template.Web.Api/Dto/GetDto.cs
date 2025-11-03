namespace Template.Web.Api.Dto;

public record GetDto : IDto
{
    public Guid Id { get; init; }
}