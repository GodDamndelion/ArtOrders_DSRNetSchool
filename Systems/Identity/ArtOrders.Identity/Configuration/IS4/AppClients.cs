namespace ArtOrders.Identity.Configuration;

using ArtOrders.Common.Security;
using Duende.IdentityServer.Models;

public static class AppClients
{
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client // - это те приложения, которые будут иметь право работать с нашим Identity сервером
            {
                // TODO: Поменять Скопы
                ClientId = "swagger",
                ClientSecrets =
                {
                    new Secret("secret".Sha256()) //"secret" должен быть секретным, большим и скрыт где-нибудь
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials, //Авторизация только клиента (Межсервисное взаимодействие может делаться)

                AccessTokenLifetime = 3600, // 1 hour

                AllowedScopes = {
                    AppScopes.BooksRead,
                    AppScopes.BooksWrite
                }
            }
            ,
            new Client
            {
                ClientId = "frontend",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt,

                AccessTokenLifetime = 3600, // 1 hour

                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = 2592000, // 30 days
                SlidingRefreshTokenLifetime = 1296000, // 15 days

                // TODO: Поменять книжные скопы

                AllowedScopes = {
                    AppScopes.BooksRead,
                    AppScopes.BooksWrite
                }
            }
        };
}