namespace ArtOrders.Services.Chats;

public interface IChatService
{
    Task<IEnumerable<ChatModel>> GetChats(int offset = 0, int limit = 10);
    Task<ChatModel> GetChat(int chatId);
    Task<ChatModel> AddChat(AddChatModel model);
    Task UpdateChat(int id, UpdateChatModel model);
    Task DeleteChat(int chatId);
}
