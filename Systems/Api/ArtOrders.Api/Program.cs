using ArtOrders.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppLogger();

// Configure services

var services = builder.Services;

services.AddHttpContextAccessor(); //Для CorrelationId
services.AddAppCors();

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger();
services.AddAppAutoMappers();

//builder.Services.AddControllers(); Меняем стандартный на
services.AddAppControllers();


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseAuthorization();

//app.MapControllers(); Меняем стандартные на
app.UseAppControllers();

app.Run();
