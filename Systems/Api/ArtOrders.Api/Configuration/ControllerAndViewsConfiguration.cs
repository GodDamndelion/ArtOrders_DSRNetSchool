namespace ArtOrders.Api.Configuration;

using ArtOrders.Common;

public static class ControllerAndViewsConfiguration
{
    public static IServiceCollection AddAppControllers(this IServiceCollection services)
    {
        services
            .AddRazorPages();

        services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultSettings())
            .AddValidator()
            ;

        return services;
    }

    public static IEndpointRouteBuilder UseAppControllers(this IEndpointRouteBuilder app)
    {
        app.MapRazorPages();
        app.MapControllers();

        return app;
    }
}