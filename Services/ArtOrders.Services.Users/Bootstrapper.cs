namespace ArtOrders.Services.Users;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddUserService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();  // !!!  UserService должен объявляться как SCOPED

        return services;
    }
}
