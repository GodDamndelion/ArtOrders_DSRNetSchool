namespace ArtOrders.Api;

using ArtOrders.Api.Settings;
using ArtOrders.Services.Orders;
// TODO: При добавлении сервисов подключать их тут!!!
//using ArtOrders.Services.Actions;
using ArtOrders.Services.Cache;
//using ArtOrders.Services.RabbitMq;
using ArtOrders.Services.Settings;
using ArtOrders.Services.Users;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddIdentitySettings()
            .AddApiSpecialSettings()
            .AddOrderService()
            .AddUserService()
            .AddCache()
            //.AddRabbitMq()
            //.AddActions()
            ;

        return services;
    }
}
