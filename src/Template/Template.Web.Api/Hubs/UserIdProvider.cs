using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Template.Web.Api.Hubs;

public class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        var claims = connection.User?.Claims;
        return claims?.FirstOrDefault(c=> c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}