using System;
using System.Linq;
using gdsc_web_backend.Database;
using gdsc_web_backend.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace gdsc_web_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'V");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            var connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            // if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            // {
            //     services.AddLettuceEncrypt();
            // }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.Use(async (req, next) =>
            {
                Console.WriteLine($"{req.Request.Scheme} {req.Request.Host}");

                await next();
            });

            if (ShouldMigrate())
            {
                Console.WriteLine("Applying migrations...");
                using var scope = app.ApplicationServices.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                dbContext?.Database.MigrateAsync().Wait();
                Console.WriteLine("Done!");
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
            });

            // if(!env.IsDevelopment()) app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static bool ShouldMigrate()
        {
            var args = Environment.GetCommandLineArgs();
            return args.Contains("--migrate") || args.Contains("migrate");
        }
    }
}
