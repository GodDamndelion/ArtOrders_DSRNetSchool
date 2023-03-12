namespace ArtOrders.Context;

using Microsoft.EntityFrameworkCore;

public static class DbContextOptionsFactory
{
    private const string migrationProjctPrefix = "ArtOrders.Context.Migrations"; //Название проекта, в который будут складываться миграции

    public static DbContextOptions<MainDbContext> Create(string connStr)
    {
        var bldr = new DbContextOptionsBuilder<MainDbContext>();

        Configure(connStr).Invoke(bldr);

        return bldr.Options;
    }

    public static Action<DbContextOptionsBuilder> Configure(string connStr)
    {
        return (bldr) =>
        {
            bldr.UseNpgsql(connStr,
                opts => opts
                        .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                        .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                        .MigrationsAssembly($"{migrationProjctPrefix}PostgreSQL") //Путь для складывания миграций
                );

            bldr.EnableSensitiveDataLogging(); //Будут проскакивать параметры в запросах, чтобы удобнее отлаживать (А не просто что-то выполнилось)
            //bldr.UseLazyLoadingProxies(); //Не подружились
            bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); //Чтобы ничего лишнего не хранить в памяти
        };
    }
}
