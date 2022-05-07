using InteractiveHelper.Identity.Configuration;
using InteractiveHelper.Settings;

var builder = WebApplication.CreateBuilder(args);

var settings = new IS4Settings(new SettingsSource());

// Add services to the container.

var services = builder.Services;

services.AddAppCors();
services.AddAppDbContext(settings.Db);
services.AddHttpContextAccessor();
services.AddSettings();
services.AddIS4();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAppCors();
app.UseStaticFiles();
app.UseRouting();
app.UseAppDbContext();
app.UseIS4();

app.Run();
