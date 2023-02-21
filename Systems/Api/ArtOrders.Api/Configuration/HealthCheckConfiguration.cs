namespace ArtOrders.Api.Configuration;

using ArtOrders.Common;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>("ArtOrders.API");

        return services;
    }

    public static void UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health"); // Обычный health не должен ни от чего зависеть (От БД и т.п.)

        app.MapHealthChecks("/health/detail", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckHelper.WriteHealthCheckResponse,
            AllowCachingResponses = false,
        });
    }
}