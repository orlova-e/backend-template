using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Domain.Core.Entities;
using Template.Web.Api.Dto;
using Template.Web.Api.Services.Commands;
using Template.Web.Api.Services.Commands.Requests;

namespace Template.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TemplateController<TEntity, TCreateDto, TEditorDto, TViewDto, TTableDto> : ControllerBase
    where TEntity : EntityBase
    where TViewDto : GetDto
{
    private readonly IMediator _mediator;

    public TemplateController(IMediator mediator)
        => _mediator = mediator;
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetNoteRequest<TEntity, TViewDto>(id));
        return this.Unwrap<TViewDto>(result);
    }
}