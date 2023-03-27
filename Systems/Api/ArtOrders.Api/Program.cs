using ArtOrders.Api;
using ArtOrders.Api.Configuration;
using ArtOrders.Context;
using ArtOrders.Services.Settings;
using ArtOrders.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var mainSettings = Settings.Load<MainSettings>("Main");
var identitySettings = Settings.Load<IdentitySettings>("Identity");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

builder.AddAppLogger();

// Configure services

var services = builder.Services;

services.AddHttpContextAccessor(); //Для CorrelationId
services.AddAppCors();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger(identitySettings, swaggerSettings);
services.AddAppAutoMappers();

//builder.Services.AddControllers(); Меняем стандартный на
services.AddAppControllers();

services.RegisterAppServices();


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseAuthorization();

//app.MapControllers(); Меняем стандартные на
app.UseAppControllers();

app.UseAppMiddlewares();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);

app.Run();
