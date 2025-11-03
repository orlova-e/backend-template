using FluentValidation;

namespace Template.Web.Api.Services.Validation;

public interface IDtoValidator<in TRequest>: IValidator<TRequest>
{
}