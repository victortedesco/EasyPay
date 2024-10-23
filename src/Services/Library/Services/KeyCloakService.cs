﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyPay.Library.Services;

public class KeyCloakService(IHttpContextAccessor contextAccessor) : IKeyCloakService
{
    private readonly IHttpContextAccessor _httpContextAccessor = contextAccessor;

    public Guid GetUserId()
    {
        try
        {
            return Guid.Parse(_httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        }
        catch (Exception)
        {
            return Guid.Empty;
        }
    }

    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
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