namespace ArtOrders.Services.Orders;

using AutoMapper;
using ArtOrders.Common.Exceptions;
using ArtOrders.Common.Validator;
using ArtOrders.Context;
using ArtOrders.Context.Entities;
using ArtOrders.Services.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

internal class OrderService : IOrderService
{
    private const string contextCacheKey = "orders_cache_key";

    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly ICacheService cacheService;
    private readonly IModelValidator<AddOrderModel> addOrderModelValidator;
    private readonly IModelValidator<UpdateOrderModel> updateOrderModelValidator;
    private readonly ILogger<OrderService> logger;

    public OrderService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        ICacheService cacheService,
        IModelValidator<AddOrderModel> addOrderModelValidator,
        IModelValidator<UpdateOrderModel> updateOrderModelValidator,
        ILogger<OrderService> logger
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.cacheService = cacheService;
        this.addOrderModelValidator = addOrderModelValidator;
        this.updateOrderModelValidator = updateOrderModelValidator;
        this.logger = logger;
    }

    public async Task<IEnumerable<OrderModel>> GetOrders(int offset = 0, int limit = 10)
    {
        // Пока (не) закроем кэширование на время отладки
        try
        {
            // Пока кэш не устареет, будут показываться одни и те же данные, даже если в БД они уже изменились (Избегать этого!)
            // Кэш используется для данных, которые долго доставать из БД и которые гарантированно не поменяются за время его жизни или если изменения не будут критичными.
            // Также он может использоваться для каких-либо расчётных данных, например для матрицы прав на время жизни сессии.
            // Ещё кэш может использоваться для настроек.
            // Кэш может применяться, когда идёт огромное количество запросов. Например, время жизни кэша 1 минута, и за это время делаются тысячи или миллионы запросов.
            var cached_data = await cacheService.Get<IEnumerable<OrderModel>>(contextCacheKey);
            if (cached_data != null)
                return cached_data; // Если нашли данные в кэше, то тут же вернули. Иначе...
        }
        catch (Exception ex)
        {
            // Log message from exception message (CacheService.Get)
            // "Не исключение, так как Кэш - это не что-то критичное"
            logger.LogWarning("CacheService exception: ", ex);
        }

        await Task.Delay(3000); //Эмуляция долгой работы

        using var context = await contextFactory.CreateDbContextAsync();

        var orders = context
            .Orders
            .Include(x => x.CurrentResultImage)
            .Include(x => x.Chat)
            .AsQueryable();

        orders = orders
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await orders.ToListAsync()).Select(order => mapper.Map<OrderModel>(order)); //Сформировали

        await cacheService.Put(contextCacheKey, data, TimeSpan.FromSeconds(30)); //И положили в кэш

        return data;
    }

    public async Task<OrderModel> GetOrder(int id)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var order = await context.Orders.Include(x => x.CurrentResultImage).Include(x => x.Chat).FirstOrDefaultAsync(x => x.Id.Equals(id));

        var data = mapper.Map<OrderModel>(order);

        return data;
    }

    public async Task<OrderModel> AddOrder(AddOrderModel model)
    {
        addOrderModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var order = mapper.Map<Order>(model);

        await context.Orders.AddAsync(order);
        context.SaveChanges();

        await cacheService.Delete(contextCacheKey);

        return mapper.Map<OrderModel>(order);
    }

    public async Task UpdateOrder(int orderId, UpdateOrderModel model)
    {
        updateOrderModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var order = await context.Orders.FirstOrDefaultAsync(x => x.Id.Equals(orderId));

        ProcessException.ThrowIf(() => order is null, $"The order (id: {orderId}) was not found");

        order = mapper.Map(model, order);

        context.Orders.Update(order);
        context.SaveChanges();
        await cacheService.Delete(contextCacheKey);
    }

    public async Task DeleteOrder(int orderId)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var order = await context.Orders.FirstOrDefaultAsync(x => x.Id.Equals(orderId))
            ?? throw new ProcessException($"The order (id: {orderId}) was not found");

        context.Remove(order);
        context.SaveChanges();
        await cacheService.Delete(contextCacheKey);
    }
}
