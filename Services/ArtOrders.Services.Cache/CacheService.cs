namespace ArtOrders.Services.Cache;

using ArtOrders.Common;
using ArtOrders.Common.Extensions;
using StackExchange.Redis;

public class CacheService : ICacheService
{
    private TimeSpan defaultLifetime = TimeSpan.FromMinutes(1);

    private readonly CacheSettings settings;
    private readonly IDatabase cacheDb;

    private static string redisUri;
    private static ConnectionMultiplexer Connection => lazyConnection.Value; //Будет создано в момент обращения, а не при запуске
    private static Lazy<ConnectionMultiplexer> lazyConnection = new(() => ConnectionMultiplexer.Connect(redisUri));

    public CacheService(CacheSettings settings)
    {
        this.settings = settings;

        redisUri = this.settings.Uri;
        defaultLifetime = TimeSpan.FromMinutes(this.settings.CacheLifeTime);

        cacheDb = Connection.GetDatabase();
    }

    public string KeyGenerate()
    {
        return Guid.NewGuid().Shrink(); //Сокращение '-' и ' ' из ArtOrders.Common.Extensions
    }

    public async Task<bool> Delete(string key)
    {
        return await cacheDb.KeyDeleteAsync(key);
    }

    public async Task<T> Get<T>(string key, bool resetLifeTime = false)
    {
        try
        {
            string cached_data = await cacheDb.StringGetAsync(key);
            if (cached_data.IsNullOrEmpty())
                return default;

            var data = cached_data.FromJsonString<T>(); //В Redis хранятся json

            if (resetLifeTime)
                await SetStoreTime(key);

            return data;
        }
        catch (Exception ex)
        {
            throw new Exception($"Can`t get data from cache for {key}", ex);
        }
    }

    public async Task<bool> Put<T>(string key, T data, TimeSpan? storeTime = null)
    {
        return await cacheDb.StringSetAsync(key, data.ToJsonString(), storeTime ?? defaultLifetime);
    }

    public async Task SetStoreTime(string key, TimeSpan? storeTime = null)
    {
        await cacheDb.KeyExpireAsync(key, storeTime ?? defaultLifetime);
    }
}
