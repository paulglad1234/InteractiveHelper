using InteractiveHelper.Api;
using InteractiveHelper.Api.Configuration;
using Serilog;
using InteractiveHelper.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((host, config) =>
{
    config
    .Enrich.WithCorrelationId()
    .ReadFrom.Configuration(host.Configuration);
});

var settings = new ApiSettings(new SettingsSource());

var services = builder.Services;
// Add services to the container.

services.AddHttpContextAccessor();

//services.AddRouting();

services.AddAppDbContext(settings);

services.AddAppHealthChecks();

services.AddAppVersions();

services.AddAppCors();

services.AddAppAuth(settings);

services.AddControllers().AddValidator();

services.AddAppServices();

services.AddAppSwagger(settings);

services.AddAppAutoMapper();

var app = builder.Build();

app.UseAppMiddlewares();

app.UseRouting();

app.UseAppHealthChecks();

app.UseAppCors();

app.UseSerilogRequestLogging();

app.UseAppAuth();

app.UseAppSwagger();

app.UseEndpoints(c => c.MapControllers());

app.UseAppDbContext();

app.Run();
