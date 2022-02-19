using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GdscBackend.Utils.Extensions;

public static class MigrationMiddleware
{
    /// <summary>
    /// Migrates the database if --migrate option is passed to dotnet run.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
    /// <returns>A boolean indicating if migration occured or not</returns>
    public static bool MigrateIfNeeded(this IApplicationBuilder app)
    {
        return ShouldMigrate() && app.Migrate();
    }

    private static bool Migrate(this IApplicationBuilder app)
    {
        Console.WriteLine("Applying migrations...");

        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<DbContext>();
        if (dbContext is not null)
        {
            dbContext.Database.MigrateAsync().Wait();
            Console.WriteLine("Done!");

            return true;
        }

        Console.WriteLine("DbContext is null. No migrations run!");
        return false;
    }

    private static bool ShouldMigrate()
    {
        var args = Environment.GetCommandLineArgs();
        return args.Contains("--migrate") || args.Contains("migrate");
    }
}
