using FluentValidation;

namespace Template.Web.Api.Services.Accessors;

public abstract class AccessValidator<TRequest> : AbstractValidator<TRequest>, IAccessValidator<TRequest>
{
}