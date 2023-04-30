namespace ArtOrders.Web;

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ChatService : IChatService
{
    private readonly HttpClient _httpClient;

    public ChatService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ChatListItem>> GetMyChats(int offset = 0, int limit = 100)
    {
        string url = $"{Settings.ApiRoot}/v1/chats?offset={offset}&limit={limit}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<IEnumerable<ChatListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ChatListItem>();

        return data;
    }

    public async Task<ChatListItem> GetChat(int chatId)
    {
        string url = $"{Settings.ApiRoot}/v1/chats/{chatId}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<ChatListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ChatListItem();

        return data;
    }

    public async Task AddChat(ChatModel model)
    {
        string url = $"{Settings.ApiRoot}/v1/chats";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, request);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditChat(int chatId, ChatModel model)
    {
        string url = $"{Settings.ApiRoot}/v1/chats/{chatId}";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(url, request);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteChat(int chatId)
    {
        string url = $"{Settings.ApiRoot}/v1/chats/{chatId}";

        var response = await _httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}
