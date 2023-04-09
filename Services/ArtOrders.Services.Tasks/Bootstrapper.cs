namespace ArtOrders.Services.Tasks;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddTaskService(this IServiceCollection services)
    {
        services.AddSingleton<ITaskService, TaskService>();

        return services;
    }
}
