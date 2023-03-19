namespace ArtOrders.Services.Orders;

public interface IOrderService
{
    Task<IEnumerable<OrderModel>> GetOrders(int offset = 0, int limit = 10);
    Task<OrderModel> GetOrder(int orderId);
    Task<OrderModel> AddOrder(AddOrderModel model);
    Task UpdateOrder(int id, UpdateOrderModel model);
    Task DeleteOrder(int orderId);
}
