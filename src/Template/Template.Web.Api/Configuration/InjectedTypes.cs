using System.Collections.Immutable;
using Template.Domain.Core.Entities;
using Template.Web.Api.Dto;

namespace Template.Web.Api.Configuration;

internal static class InjectedTypes
{
    public static ImmutableList<ConfigurationType> NotesTypes { get; }

    static InjectedTypes()
    {
        NotesTypes = new List<ConfigurationType>
        {
            new()
            {
                Entity = typeof(Entity),
                ViewDto = typeof(NoteGetDto),
            },
        }.ToImmutableList();
    }
}