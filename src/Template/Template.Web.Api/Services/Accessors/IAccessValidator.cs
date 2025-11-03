using FluentValidation;

namespace Template.Web.Api.Services.Accessors;

public interface IAccessValidator<in TRequest>: IValidator<TRequest>
{
}