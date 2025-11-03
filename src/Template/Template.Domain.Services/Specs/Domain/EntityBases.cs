using Template.Domain.Core.Entities;

namespace Template.Domain.Services.Specs.Domain;

public static class EntityBases<T>
    where T : EntityBase
{
    public static Spec<T> ByName(string name) => new(x => x.Name == name);
}