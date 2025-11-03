using FluentValidation;
using Template.Domain.Core.Entities;
using Template.Domain.Core.Interfaces;
using Template.Infrastructure.DataAccess;
using Template.Services.Shared.Interfaces;

namespace Template.Web.Api.Services.Validation.Helpers;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<TEditorDto, Guid> Exists<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetAsyncValidator(new ExistsValidator<TEntity, TEditorDto>(repository));
    }
    
    public static IRuleBuilderOptions<TEditorDto, Guid?> ExistsOrDefault<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid?> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetAsyncValidator(new ExistsOrDefaultValidator<TEntity, TEditorDto>(repository));
    }
    
    public static IRuleBuilderOptions<TEditorDto, Guid> ExistsOrEmpty<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetAsyncValidator(new ExistsOrEmptyValidator<TEntity, TEditorDto>(repository));
    }
    
    public static IRuleBuilderOptions<TEditorDto, Guid> NotExists<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetAsyncValidator(new NotExistsValidator<TEntity, TEditorDto>(repository));
    }
}