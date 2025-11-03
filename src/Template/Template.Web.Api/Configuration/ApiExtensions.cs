using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Template.Services.Shared.Interfaces;
using Template.Web.Api.Hubs;
using Template.Web.Api.Models;
using Template.Web.Api.Services.Accessors;
using Template.Web.Api.Services.Commands;
using Template.Web.Api.Services.Commands.Requests;
using Template.Web.Api.Services.Commands.Requests.Handlers;
using Template.Web.Api.Services.Conventions;
using Template.Web.Api.Services.FeatureProviders;
using Template.Web.Api.Services.Implementation;
using Template.Web.Api.Services.PipelineBehaviours;
using Template.Web.Api.Services.Validation;
using ApiHubOptions = Template.Web.Api.Models.HubOptions;

namespace Template.Web.Api.Configuration;

public static class ApiExtensions
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var identityOptions = configuration
            .GetSection(nameof(IdentityOptions))
            .Get<IdentityOptions>();
        
        var hubOptions = configuration
            .GetSection(nameof(Microsoft.AspNetCore.SignalR.HubOptions))
            .Get<ApiHubOptions>();

        services
            .AddSignalR()
            .AddHubOptions<NotesHub>(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(hubOptions.ClientTimeoutInterval);
                options.KeepAliveInterval = TimeSpan.FromSeconds(hubOptions.KeepAliveInterval);
            })
            .Services
            .AddSingleton<IUserIdProvider, UserIdProvider>();
        
        services
            .AddControllers().Services
            .AddOptions()
            .AddSwagger(configuration)
            .AddMvcCore(o => o.Conventions.Add(new NotesControllerConvention()))
            .ConfigureApplicationPartManager(c => c.FeatureProviders.Add(new TemplateControllerFeatureProvider())).Services
            .AddAutoMapper(typeof(Program).Assembly)
            .AddValidatorsFromAssemblyContaining<Program>()
            .AddValidators(typeof(IDtoValidator<>))
            .AddValidators(typeof(IAccessValidator<>))
            .AddMediatR(typeof(GetNoteRequest<,>).Assembly)
            .AddGenericRequestHandlers()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AccessorBehavior<,>))
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = identityOptions.Address;
                options.RequireHttpsMetadata = identityOptions.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = identityOptions.ValidateAudience,
                    ValidateIssuer = identityOptions.ValidateIssuer
                };
            })
            .Services
            .AddAuthorization()
            //.AddTransient<ICurrentUserService, CurrentUserService>()
            .AddHttpContextAccessor()
            .TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        
        return services;
    }
    
    private static IServiceCollection AddValidators(
        this IServiceCollection services,
        Type requiredType)
    {
        var types = typeof(Program).Assembly
            .GetTypes()
            .Where(type => !type.IsAbstract &&
                           !type.IsGenericTypeDefinition)
            .Select(x => new
            {
                Type = x, 
                Interfaces = x.GetInterfaces()
            })
            .Select(x => new
            {
                x.Type,
                GenericInterfaces = x.Interfaces.Where(e => 
                    e.GetTypeInfo().IsGenericType &&
                    e.GetGenericTypeDefinition() == requiredType)
            })
            .Select(x => new
            {
                x.Type, 
                MatchingInterface = x.GenericInterfaces.FirstOrDefault()
            })
            .Where(x => x.MatchingInterface != null)
            .Select(x => new
            {
                InterfaceType = x.MatchingInterface,
                ImplementationType = x.Type
            })
            .ToArray();

        foreach (var type in types)
        {
            services.AddTransient(type.InterfaceType, type.ImplementationType);
            services.AddTransient(type.ImplementationType);
        }

        return services;
    }

    private static IServiceCollection AddGenericRequestHandlers(this IServiceCollection services)
    {
        foreach (var type in InjectedTypes.NotesTypes)
        {
            #region get

            var getRequestImplementationType = typeof(GetNoteRequest<,>).MakeGenericType(type.Entity, type.ViewDto);
            
            var getCommandInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(
                getRequestImplementationType,
                typeof(HandlerResult<>).MakeGenericType(type.ViewDto));

            var getCommandImplementationType = typeof(GetNoteCommand<,>).MakeGenericType(type.Entity, type.ViewDto);
            
            services.AddTransient(getCommandInterfaceType, getCommandImplementationType);

            #endregion
        }
        
        return services;
    }

    private static IServiceCollection AddSwagger(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddSwaggerGen();
    }
}