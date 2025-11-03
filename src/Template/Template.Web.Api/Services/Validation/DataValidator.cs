using FluentValidation;

namespace Template.Web.Api.Services.Validation;

public abstract class DataValidator<TDto> : AbstractValidator<TDto>, IDtoValidator<TDto>
{
}