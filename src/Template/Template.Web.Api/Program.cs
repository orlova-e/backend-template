using Microsoft.AspNetCore.HttpOverrides;
using Template.Infrastructure.Base.Configuration;
using Template.Infrastructure.DataAccess.Configuration;
using Template.Services.Shared.Configuration;
using Template.Web.Api.Configuration;
using Template.Web.Api.Hubs;
using Template.Web.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddBaseInfrastructure(builder.Configuration)
    .AddCommonInfrastructure()
    .AddSharedServices(typeof(Program).Assembly)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app
    .UseForwardedHeaders(new ForwardedHeadersOptions 
        {ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto})
    .UseRouting()
    .UseHttpsRedirection()
    .UseLoggingMiddleware()
    .UseAuthentication()
    .UseAuthorization()
    .UseSwagger()
    .UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.DisplayOperationId();
    })
    .UseEndpoints(endpoints =>
    {
        endpoints.MapHub<NotesHub>("/hubs");
        endpoints.MapControllers();
    });

app.Run();