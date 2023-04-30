namespace ArtOrders.Api;

// TODO: При добавлении сервисов подключать их тут!!!
using ArtOrders.Api.Settings;
using ArtOrders.Services.Orders;
using ArtOrders.Services.Chats;
using ArtOrders.Services.Tasks;
using ArtOrders.Services.Cache;
using ArtOrders.Services.RabbitMq;
using ArtOrders.Services.Settings;
using ArtOrders.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using ArtOrders.Services.Messages;

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
            .AddChatService()
            .AddUserService()
            .AddCache()
            .AddRabbitMq()
            .AddTaskService()
            .AddMessageService()
            ;

        return services;
    }
}
