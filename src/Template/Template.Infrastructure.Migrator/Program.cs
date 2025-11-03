using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template.Infrastructure.Migrator.Migrations;

namespace Template.Infrastructure.Migrator;

internal static class Program
{
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(configure =>
            {
                var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
    
                configure
                    .AddJsonFile($"appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{environment}.json", optional: false)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args);
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                
                services
                    .AddFluentMigratorCore()
                    .ConfigureRunner(r => r
                        .AddPostgres()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(NTS1).Assembly).For.Migrations())
                    .AddLogging(l => l.AddFluentMigratorConsole())
                    .BuildServiceProvider(false);
            });
}