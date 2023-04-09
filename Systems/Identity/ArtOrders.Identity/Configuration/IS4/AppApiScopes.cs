namespace ArtOrders.Identity.Configuration;

using ArtOrders.Common.Security;
using Duende.IdentityServer.Models;
// TODO: Добавить в список Скопы (Политики)!
public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.OrdersRead, "Access to orders API - Read data"),
            new ApiScope(AppScopes.OrdersWrite, "Access to orders API - Write data")
        };
}
