using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Template.Services.Shared.Implementation;
using Template.Services.Shared.Interfaces;

namespace Template.Services.Shared.Configuration;

public static class SharedServicesExtensions
{
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services,
        Assembly mappingAssembly = null)
    {
        services
            .AddSingleton<IEncryptionService, EncryptionService>()
            .AddSingleton<IDateTimeService, DateTimeService>()
            .AddSingleton<IImageService, ImageService>();

        if (mappingAssembly is not null)
        {
            services
                .AddTransient<ITranslator, Translator>()
                .AddAutoMapper(mappingAssembly);
        }

        return services;
    }
}