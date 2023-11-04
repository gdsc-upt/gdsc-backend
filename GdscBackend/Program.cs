using GdscBackend.Database;
using GdscBackend.Swagger;
using GdscBackend.Utils;
using GdscBackend.Utils.Mappers;
using GdscBackend.Utils.Services;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddAutoMapper(typeof(MappingProfiles));

services.AddControllers();
services.AddCors(options => options.AddPolicy("EnableAll", policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var keycloakOptions = configuration
    .GetRequiredSection(ConfigurationConstants.ConfigurationPrefix)
    .Get<KeycloakInstallationOptions>();

if (keycloakOptions is null) throw new Exception("keyCloakAdminConfiguartions is null");

services.AddSwaggerConfiguration(keycloakOptions);

services.AddKeycloakAuthentication(configuration);
services.AddAuthorization(
    o => o.AddPolicy(AuthorizeConstants.CoreTeam, b => { b.RequireRealmRoles("GDSC_CORE_TEAM"); }));
services.AddKeycloakAuthorization(configuration);

services.AddTransient<IEmailSender, EmailSender>();
services.AddTransient<IWebhookService, WebhookService>();

var connectionString = configuration.GetConnectionString("Default");
services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MigrateIfNeeded();

if (builder.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseCors("EnableAll");

app.UseRouting();

app.UseSwaggerMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();