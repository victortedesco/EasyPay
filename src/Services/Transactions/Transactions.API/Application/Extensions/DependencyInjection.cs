using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EasyPay.Library.Services;

namespace Transactions.API.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressMapClientErrors = true;
        }); ;
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddScoped<IKeyCloakService, KeyCloakService>();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.Audience = configuration["Authentication:Audience"];
                o.MetadataAddress = configuration["Authentication:MetadataAddress"]!;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Authentication:ValidIssuer"],
                };
            });

        services.AddSwaggerGen(o =>
        {
            o.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Keycloak:AuthorizationUrl"]!),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "openid" }
                        }
                    }
                }
            });
            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Keycloak"
                        },
                        In = ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = "Bearer"
                    },
                    []
                }
            };
            o.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}
