using MediatR;
using Template.Web.Api.Dto;

namespace Template.Web.Api.Services.Commands.Requests;

public class GetNoteRequest<TEntity, TViewDto>(Guid entityId) : IRequest<HandlerResult<TViewDto>>
    where TViewDto : GetDto
{
    public Guid EntityId { get; } = entityId;
}