using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyPay.Library.Services;

public class KeyCloakService(IHttpContextAccessor contextAccessor) : IKeyCloakService
{
    private readonly IHttpContextAccessor _httpContextAccessor = contextAccessor;

    public Guid? GetUserId()
    {
        var sub = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        if (Guid.TryParse(sub, out var userId))
        {
            return userId;
        }

        return null;
    }

    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
    }

    public string GetUserDisplayName()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "fullname")?.Value;
    }

    public IEnumerable<string> GetUserRoles()
    {
        var realmAccessClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "realm_access")?.Value;

        if (string.IsNullOrEmpty(realmAccessClaim))
        {
            return [];
        }

        try
        {
            var realmAccess = JsonSerializer.Deserialize<RealmAccess>(realmAccessClaim);
            return realmAccess?.Roles ?? [];
        }
        catch (JsonException)
        {
            return [];
        }
    }

    public string GetUserEmail()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }
}

internal class RealmAccess
{
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
}