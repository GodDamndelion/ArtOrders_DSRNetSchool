namespace ArtOrders.EmailSenderWorker;

using ArtOrders.Services.EmailSender;
using ArtOrders.Services.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddRabbitMq()            
            .AddEmailSender()
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}
