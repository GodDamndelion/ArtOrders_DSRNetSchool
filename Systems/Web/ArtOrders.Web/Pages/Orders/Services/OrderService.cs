namespace ArtOrders.Web;

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<OrderListItem>> GetOrders(int offset = 0, int limit = 10)
    {
        string url = $"{Settings.ApiRoot}/v1/orders?offset={offset}&limit={limit}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<IEnumerable<OrderListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<OrderListItem>();

        return data;
    }

    public async Task<OrderListItem> GetOrder(int orderId)
    {
        string url = $"{Settings.ApiRoot}/v1/orders/{orderId}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<OrderListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new OrderListItem();

        return data;
    }

    //public async Task AddOrder(OrderModel model)
    //{
    //    string url = $"{Settings.ApiRoot}/v1/orders";

    //    var body = JsonSerializer.Serialize(model);
    //    var request = new StringContent(body, Encoding.UTF8, "application/json");
    //    var response = await _httpClient.PostAsync(url, request);

    //    var content = await response.Content.ReadAsStringAsync();

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        throw new Exception(content);
    //    }
    //}

    //public async Task EditOrder(int orderId, OrderModel model)
    //{
    //    string url = $"{Settings.ApiRoot}/v1/orders/{orderId}";

    //    var body = JsonSerializer.Serialize(model);
    //    var request = new StringContent(body, Encoding.UTF8, "application/json");

    //    var response = await _httpClient.PutAsync(url, request);

    //    var content = await response.Content.ReadAsStringAsync();

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        throw new Exception(content);
    //    }
    //}

    //public async Task DeleteOrder(int orderId)
    //{
    //    string url = $"{Settings.ApiRoot}/v1/orders/{orderId}";

    //    var response = await _httpClient.DeleteAsync(url);
    //    var content = await response.Content.ReadAsStringAsync();

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        throw new Exception(content);
    //    }
    //}
}
