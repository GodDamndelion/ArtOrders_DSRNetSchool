using ArtOrders.Context;
using ArtOrders.Identity;
using ArtOrders.Identity.Configuration;

//Не выбрали Использовать контроллеры, поэтому все начальные классы были в файле Program.cs

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

var services = builder.Services;

services.AddAppCors();

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();

services.RegisterAppServices();

services.AddIS4(); //Identity Server 4


var app = builder.Build();

app.UseAppHealthChecks();

app.UseIS4();


app.Run();
