using Template.Domain.Core.Interfaces;

namespace Template.Services.Shared.Interfaces;

public interface IDateTimeService
{
    DateTime UtcNow { get; }
    void Created<T>(T entity) where T : class, IHistorical;
    void Updated<T>(T entity) where T : class, IHistorical;
    void Deleted<T>(T entity) where T : class, IHistorical;
}