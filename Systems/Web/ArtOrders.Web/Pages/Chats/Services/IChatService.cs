namespace ArtOrders.Web;

using System.Threading.Tasks;

public interface IChatService
{
    Task<IEnumerable<ChatListItem>> GetMyChats(int offset = 0, int limit = 10);
    Task<ChatListItem> GetChat(int chatId);
    Task AddChat(ChatModel model);
    Task EditChat(int chatId, ChatModel model);
    Task DeleteChat(int chatId);
}
