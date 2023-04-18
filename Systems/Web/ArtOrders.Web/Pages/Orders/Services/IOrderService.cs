namespace ArtOrders.Web;

using System.Threading.Tasks;

public interface IOrderService
{
    Task<IEnumerable<OrderListItem>> GetOrders(int offset = 0, int limit = 10);
    Task<OrderListItem> GetOrder(int orderId);
    //Task AddOrder(OrderModel model);
    //Task EditOrder(int orderId, OrderModel model);
    //Task DeleteOrder(int orderId);
}
