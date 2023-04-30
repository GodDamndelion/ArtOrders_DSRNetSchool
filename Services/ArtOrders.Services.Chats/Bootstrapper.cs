namespace ArtOrders.Services.Chats;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddChatService(this IServiceCollection services)
    {
        services.AddSingleton<IChatService, ChatService>();

        return services;
    }
}
