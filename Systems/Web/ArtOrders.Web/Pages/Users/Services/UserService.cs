using System.Net.Http;
using System.Text.Json;

namespace ArtOrders.Web;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<UserListItem>> GetArtists(int offset = 0, int limit = 10)
    {
        string url = $"{Settings.ApiRoot}/v1/users?offset={offset}&limit={limit}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<IEnumerable<UserListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<UserListItem>();

        return data;
    }

    public async Task<UserListItem> GetCurrentUser()
    {
        string url = $"{Settings.ApiRoot}/v1/users/current";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<UserListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new UserListItem();

        return data;
    }
}
