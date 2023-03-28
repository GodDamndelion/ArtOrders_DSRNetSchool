﻿namespace ArtOrders.Api;

using ArtOrders.Api.Settings;
using ArtOrders.Services.Orders;
// TODO: Есть выключенные сущности. Переделать в свои.
//using ArtOrders.Services.Actions;
//using ArtOrders.Services.Authors;
//using ArtOrders.Services.Cache;
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
            //.AddCache()
            //.AddRabbitMq()
            //.AddActions()
            //.AddAuthorService()
            ;

        return services;
    }
}
