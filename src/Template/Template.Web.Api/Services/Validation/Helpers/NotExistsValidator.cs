using FluentValidation;
using FluentValidation.Validators;
using Template.Domain.Core.Interfaces;
using Template.Domain.Services.Specs.Domain;
using Template.Infrastructure.DataAccess;

namespace Template.Web.Api.Services.Validation.Helpers;

public class NotExistsValidator<TEntity, TEditorDto> : AsyncPropertyValidator<TEditorDto, Guid>
    where TEntity : class, IDomainEntity
{
    private readonly IRepository _repository;
    
    public override string Name => nameof(NotExistsValidator<TEntity, TEditorDto>);

    public NotExistsValidator(IRepository repository)
        => _repository = repository;

    public override async Task<bool> IsValidAsync(
        ValidationContext<TEditorDto> context,
        Guid value, 
        CancellationToken cancellationToken)
    {
        var count = await _repository.CountAsync<TEntity, Guid>(
            Common.NotDeleted<TEntity>(value), 
            cancellationToken);
        
        return count is 0;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => $"Entity of type '{typeof(TEntity).Name}' is already exist";
}