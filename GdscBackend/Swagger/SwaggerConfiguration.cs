using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GdscBackend.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services,
        KeycloakInstallationOptions keycloakOption)
    {
        var url = $"{keycloakOption.KeycloakUrlRealm}/protocol/openid-connect";

        var openIdConnectSecurityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Type = SecuritySchemeType.OAuth2,
            In = ParameterLocation.Header,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(url + "/auth"),
                    TokenUrl = new Uri(url + "/token"),
                    Scopes = new Dictionary<string, string> { { "openid", "openid" }, { "profile", "profile" } }
                }
            }
        };

        var securityRequirements = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "gdsc"
                    }
                },
                Array.Empty<string>()
            }
        };

        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'V");
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("gdsc", openIdConnectSecurityScheme);
            option.AddSecurityRequirement(securityRequirements);
        });
    }
}