namespace ArtOrders.Services.Messages;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddMessageService(this IServiceCollection services)
    {
        services.AddSingleton<IMessageService, MessageService>();

        return services;
    }
}
