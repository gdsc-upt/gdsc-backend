using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GdscBackend.Swagger;

public static class SwaggerMiddleware
{
    public static IApplicationBuilder UseSwaggerMiddleware(this WebApplication app)
    {
        return app.UseSwagger().UseSwaggerUI(options =>
        {
            var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            if (provider is null)
            {
                app.Logger.LogError("Api version description provider is null!");
                return;
            }

            foreach (var description in provider.ApiVersionDescriptions)
            {
                var groupName = description.GroupName;
                var url = $"/swagger/{groupName}/swagger.json";
                options.SwaggerEndpoint(url, groupName.ToUpperInvariant());
            }
        });
    }
}
