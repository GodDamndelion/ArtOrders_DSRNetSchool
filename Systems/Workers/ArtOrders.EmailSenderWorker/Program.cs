using ArtOrders.Context;
using ArtOrders.Settings;
using ArtOrders.EmailSenderWorker;
using ArtOrders.EmailSenderWorker.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppLogger();

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration); //Тут не используется

services.AddAppHealthChecks();

services.RegisterAppServices();


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppHealthChecks();


// Start task executor

app.Services.GetRequiredService<ITaskExecutor>().Start();


// Run app

app.Run();
