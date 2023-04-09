namespace ArtOrders.Api;

// TODO: При добавлении сервисов подключать их тут!!!
using ArtOrders.Api.Settings;
using ArtOrders.Services.Orders;
using ArtOrders.Services.Tasks;
using ArtOrders.Services.Cache;
using ArtOrders.Services.RabbitMq;
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
            .AddRabbitMq()
            .AddTaskService()
            ;

        return services;
    }
}
