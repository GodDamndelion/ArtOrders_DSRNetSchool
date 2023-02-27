namespace ArtOrders.Api;

//using ArtOrders.Api.Settings;
// TODO: Есть выключенные сущности. Переделать в свои.
//using ArtOrders.Services.Actions;
//using ArtOrders.Services.Authors;
//using ArtOrders.Services.Books;
//using ArtOrders.Services.Cache;
//using ArtOrders.Services.RabbitMq;
using ArtOrders.Services.Settings;
//using ArtOrders.Services.UserAccount;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            //.AddIdentitySettings()
            //.AddApiSpecialSettings()
            //.AddBookService()
            //.AddUserAccountService()
            //.AddCache()
            //.AddRabbitMq()
            //.AddActions()
            //.AddAuthorService()
            ;

        return services;
    }
}
