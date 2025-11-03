using Microsoft.Extensions.DependencyInjection;
using Template.Infrastructure.DataAccess.Implementation;

namespace Template.Infrastructure.DataAccess.Configuration;

public static class CommonInfrastructureExtensions
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services
            .AddScoped<IRepository, Repository>();

        return services;
    }
}