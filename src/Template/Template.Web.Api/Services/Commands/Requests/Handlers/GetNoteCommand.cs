using MediatR;
using Template.Web.Api.Dto;

namespace Template.Web.Api.Services.Commands.Requests.Handlers;

public class GetNoteCommand<TEntity, TViewDto> : IRequestHandler<GetNoteRequest<TEntity, TViewDto>, HandlerResult<TViewDto>>
    where TViewDto : GetDto
{
    public Task<HandlerResult<TViewDto>> Handle(GetNoteRequest<TEntity, TViewDto> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}