using ArtOrders.Api;
using ArtOrders.Api.Configuration;
using ArtOrders.Context;
using ArtOrders.Services.Settings;
using ArtOrders.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

builder.AddAppLogger();

// Configure services

var services = builder.Services;

services.AddHttpContextAccessor(); //��� CorrelationId
services.AddAppCors();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger(mainSettings, swaggerSettings);
services.AddAppAutoMappers();

//builder.Services.AddControllers(); ������ ����������� ��
services.AddAppControllers();

services.RegisterAppServices();


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseAuthorization();

//app.MapControllers(); ������ ����������� ��
app.UseAppControllers();

app.UseAppMiddlewares();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);

app.Run();
